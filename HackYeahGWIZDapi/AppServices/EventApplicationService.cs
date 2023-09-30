using HackYeahGWIZDapi.AppContext;
using HackYeahGWIZDapi.Model;
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

        public EventApplicationService(ApplicationContext applicationContext)
        {
            _context = applicationContext;
        }

        public async Task Create(Event _event)
        {
            var newEvent = new Event
            {
                Localization = _event.Localization,
                User = _event.User,
                EventPhotos = _event.EventPhotos,
                Animal = _event.Animal,
                Date = _event.Date
            };
            await _context.Events.AddAsync(newEvent);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Event>> GetAll()
        {
            var allEvents = await _context.Events.ToListAsync();
            var currentDate = DateTime.Now;
            return allEvents.Where(x => x.Date >= currentDate.AddHours(-1)).ToList();
        }

    }
}
