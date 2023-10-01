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
                var events = await _context.Events.Where(x => x.Date >= currentDate.AddHours(-1)).ToListAsync();
                return events;
            }
            catch (Exception ex)
            {
                return new List<Event>();       
            }                    
        }
        public async Task UpdateExpiredTime(Event _event)
        {
            var currentDate = DateTime.Now;
            _event.ExpiredTime = currentDate.AddHours(1);
            _context.Events.Update(_event);
            await _context.SaveChangesAsync();
        }

    }
}
