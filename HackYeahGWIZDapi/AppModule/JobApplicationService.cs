using HackYeahGWIZDapi.AppServices;
using HackYeahGWIZDapi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackYeahGWIZDapi.AppModule
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly IEventApplicationService _eventApplicationService;
        private readonly int NymberOfSameAnimals = 4;
        
        public JobApplicationService(IEventApplicationService eventApplicationService)
        {
            _eventApplicationService = eventApplicationService;
        }
        public async Task CheckMultiplyEvents()
        {
            var notExpireEvents = await _eventApplicationService.GetAllNotExpired();
            var groups = notExpireEvents.GroupBy(x => x.AnimalId).ToList();
            var eventsToUpdate = new List<Event>();
            foreach (var item in groups)
            {
                if (item.Count() >= NymberOfSameAnimals)
                {
                    //eventsToUpdate = notExpireEvents.Where(x => x.AnimalId == item)
                }
            }
        }
    }
}
