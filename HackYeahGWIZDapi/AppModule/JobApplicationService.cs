using HackYeahGWIZDapi.AppServices;
using HackYeahGWIZDapi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HackYeahGWIZDapi.AppModule
{
    public class JobApplicationService : IJobApplicationService
    {
        private readonly IEventApplicationService _eventApplicationService;
        private readonly int NymberOfSameAnimals = 4;
        private readonly int DistanceToUpdate = 1000;
        
        public JobApplicationService(IEventApplicationService eventApplicationService)
        {
            _eventApplicationService = eventApplicationService;
        }
        public async Task CheckMultiplyEvents()
        {
            var notExpireEvents = await _eventApplicationService.GetAllNotExpired();
            var groups = notExpireEvents.GroupBy(x => x.AnimalId).Select(group => new
            {
                AnimalId = group.Key, //ile mam niewygasnietych zgloszen tego samego typu
                Count = group.Count()
            })
            .ToList();
            var eventsToUpdate = new List<Event>();
            foreach (var item in groups)
            {
                if (item.Count >= NymberOfSameAnimals)
                {
                    eventsToUpdate = notExpireEvents
                        .Where(x => x.AnimalId == item.AnimalId)
                        .OrderByDescending(x => x.Date)
                        .ToList();                  

                    for (int i = 0; i < eventsToUpdate.Count(); i++)
                    {
                        var distance = CalcDistance(
                            eventsToUpdate[0].Localization.Latitude, 
                            eventsToUpdate[0].Localization.Longitude, 
                            eventsToUpdate[i+1].Localization.Latitude, 
                            eventsToUpdate[i+1].Localization.Longitude
                            );

                        if(distance < DistanceToUpdate)
                        {
                            await _eventApplicationService.UpdateExpiredTime(eventsToUpdate[i+1], eventsToUpdate[0]);
                        }

                    }
                }
            }
            
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
