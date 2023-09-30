﻿using HackYeahGWIZDapi.AppContext;
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
                AnimalId = _event.AnimalId
            };
            await _context.Events.AddAsync(newEvent);
            await _context.SaveChangesAsync();
        }
        public async Task<List<EventViewModel>> GetAll()
        {
            var allEvents = await _context.Events.ToListAsync();
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
                    Date = item.Date.ToString("yyyy-MM-dd HH:mm:ss"),
                    ExpiredTime = item.ExpiredTime.ToString("yyyy-MM-dd HH:mm:ss")

                });
            }
            return eventVieModel;
        }

    }
}
