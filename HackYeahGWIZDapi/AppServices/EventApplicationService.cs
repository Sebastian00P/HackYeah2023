using HackYeahGWIZDapi.AppContext;
using HackYeahGWIZDapi.AppModule;
using HackYeahGWIZDapi.Model;
using HackYeahGWIZDapi.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackYeahGWIZDapi.AppServices
{
    public class EventApplicationService :IEventApplicationService
    {
        private readonly ApplicationContext _context;
        private readonly IUserApplicationService _userApplicationService;

        public EventApplicationService(ApplicationContext applicationContext, IUserApplicationService userApplicationService)
        {
            _context = applicationContext;   
            _userApplicationService = userApplicationService;
        }

        public async Task Create(EventViewModel _event)
        {
            var users = await _userApplicationService.GetAll();
            var currentUser = users.FirstOrDefault(x => x.Phone == _event.User.Phone);
            if (currentUser != null)
            {
                var newEvent = new Event
                {
                    Localization = _event.Localization,
                    User = currentUser,
                    EventPhotos = _event.EventPhotos,
                    AnimalId = _event.AnimalId,
                    Date = DateTime.Now,
                    ExpiredTime = DateTime.Now.AddHours(1)
                };
                await _context.Events.AddAsync(newEvent);
                await _context.SaveChangesAsync();
            }
            else
            {
                var newEvent = new Event
                {
                    Localization = _event.Localization,
                    User = _event.User,
                    EventPhotos = _event.EventPhotos,
                    AnimalId = _event.AnimalId,
                    Date = DateTime.Now,
                    ExpiredTime = DateTime.Now.AddHours(1)
                };
                await _context.Events.AddAsync(newEvent);
                await _context.SaveChangesAsync();
            }
           
        }
        public async Task<List<EventViewModel>> GetAll()
        {
            var allEvents = await _context.Events
                .Include(x => x.Localization)
                .Include(x => x.EventPhotos)
                .Include(x => x.User)
                .ToListAsync();
            var currentDate = DateTime.Now;
            var allEventsFiltered = allEvents.Where(x => x.ExpiredTime >= currentDate).ToList();
            var eventVieModel = new List<EventViewModel>();
            foreach (var item in allEventsFiltered)
            {
                eventVieModel.Add(new EventViewModel()
                {
                    AnimalId = item.AnimalId,
                    Localization = item.Localization,
                    User = item.User,
                    EventPhotos = item.EventPhotos,
                    EventId = item.EventId,
                    Date = ((DateTimeOffset)item.Date).ToUnixTimeSeconds().ToString(), 
                    ExpiredTime = ((DateTimeOffset)item.ExpiredTime).ToUnixTimeSeconds().ToString()

                });
            }
            return eventVieModel;
        }
        public async Task<List<Event>> GetAllNotExpired()
        {
            try
            {
                var currentDate = DateTime.Now;
                var events = await _context.Events.Include(x => x.Localization).Where(x => x.Date >= currentDate.AddHours(-1)).ToListAsync();
                return events;
            }
            catch (Exception ex)
            {
                return new List<Event>();       
            }                    
        }
        public async Task UpdateExpiredTime(Event _event, Event _secondEvent)
        {
            _event.ExpiredTime = _secondEvent.ExpiredTime;
            _context.Events.Update(_event);
            await _context.SaveChangesAsync();
        }

        public static double CalcDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371; // promień Ziemi w kilometrach
            double dLat = ToRadians(lat2 - lat1);
            double dLon = ToRadians(lon2 - lon1);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = R * c * 1000; // wynik w metrach
            return d;
        }

        private static double ToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }

    }
}
