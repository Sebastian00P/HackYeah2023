using HackYeahGWIZDapi.AIToPredictPoint;
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
        private readonly IPredictionPointApplicationService _predictionPointApplicationService;
        private readonly int NymberOfSameAnimals = 4;
        private readonly int DistanceToUpdate = 1000;
        
        public JobApplicationService(IEventApplicationService eventApplicationService, 
            IPredictionPointApplicationService predictionPointApplicationService)
        {
            _eventApplicationService = eventApplicationService;
            _predictionPointApplicationService = predictionPointApplicationService;
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

                    for (int i = 0; i < eventsToUpdate.Count() - 1; i++)
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

        public async Task PredictNextLocationForEventGroupsInCloseProximity()
        {
            var notExpireEvents = await _eventApplicationService.GetAllNotExpired();
            var groupedEvents = GroupEventsInCloseProximity(notExpireEvents, 1.0); // 1.0 km

            foreach (var group in groupedEvents)
            {
                Localization predictedPoint;
                if (group.Count >= 4)
                {
                    var localizations = group.Select(x => x.Localization).ToList();
                    var latlons = new List<LatLon>();
                    foreach (var item in localizations)
                    {
                        latlons.Add(new LatLon((float)item.Latitude, (float)item.Longitude));
                    }

                    if (latlons.Count > 0)
                    {
                        predictedPoint = LatLon.ReturnPredictedPoint(latlons.ToArray());
                        var element = group.OrderByDescending((x) => x.Date).FirstOrDefault();

                        await _predictionPointApplicationService.Create(new PredictionEvent()
                            {
                                Localization = predictedPoint,
                                Photo = element.EventPhotos
                            });
                    }
                    
                    // Add to new table
                }
            }
        }

        private List<List<Event>> GroupEventsInCloseProximity(List<Event> events, double maxDistanceKm)
        {
            var groupedEvents = new List<List<Event>>();

            foreach (var evt in events)
            {
                bool addedToGroup = false;

                // Check if the event can be added to an existing group
                foreach (var group in groupedEvents)
                {
                    if (group.Any(e => CalculateDistance(e.Localization.Latitude, e.Localization.Longitude, evt.Localization.Latitude, evt.Localization.Longitude) <= maxDistanceKm))
                    {
                        group.Add(evt);
                        addedToGroup = true;
                        break;
                    }
                }

                // If not added to any existing group, create a new group
                if (!addedToGroup)
                {
                    groupedEvents.Add(new List<Event> { evt });
                }
            }

            return groupedEvents;
        }

        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double earthRadiusKm = 6371.0; // Radius of the Earth in kilometers

            var dLat = DegreesToRadians(lat2 - lat1);
            var dLon = DegreesToRadians(lon2 - lon1);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return earthRadiusKm * c;
        }

        private double DegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180.0);
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

        //public async Task PredictNewPoint()
        //{
        //    var notExpireEvents = await _eventApplicationService.GetAllNotExpired();
        //    var groups = notExpireEvents.GroupBy(x => x.AnimalId).Select(group => new
        //    {
        //        AnimalId = group.Key, //ile mam niewygasnietych zgloszen tego samego typu
        //        Count = group.Count()
        //    })
        //    .ToList();
        //    var eventsToUoCheck = new List<Event>();
        //    foreach (var item in groups)
        //    {
        //        if(item.Count >= NymberOfSameAnimals)
        //        {
        //            eventsToUoCheck = notExpireEvents
        //                .Where(x => x.AnimalId == item.AnimalId)
        //                .OrderByDescending(x => x.Date)
        //                .ToList();

        //            var latitudes = new List<double>(); // Szerokość geograficzna
        //            var longitudes = new List<double>(); // Długość geograficzna
        //            foreach (var events in eventsToUoCheck)
        //            {
        //                latitudes.Add(events.Localization.Latitude);
        //                longitudes.Add(events.Localization.Longitude);
        //            }
        //            // Znajdź najbardziej oddalony punkt na północ
        //            double maxDistance = 0;
        //            int point1Index = 0;
        //            int point2Index = 0;

        //            for (int i = 0; i < longitudes.Count - 1; i++)
        //            {
        //                for (int j = i + 1; j < longitudes.Count; j++)
        //                {

        //                    double distanceLong = CalcDistance(0, longitudes[i], 0, longitudes[j]);
        //                    double distanceLat = CalcDistance(0, longitudes[i], 0, longitudes[j]);
        //                    if(distanceLong > distanceLat)
        //                    {
        //                        if (distanceLong > maxDistance)
        //                        {
        //                            maxDistance = distanceLong;
        //                            point1Index = i;
        //                            point2Index = j;
        //                        }
        //                    }
        //                    if (distanceLat > distanceLong)
        //                    {
        //                        if (distanceLong > maxDistance)
        //                        {
        //                            maxDistance = distanceLong;
        //                            point1Index = i;
        //                            point2Index = j;
        //                        }
        //                    }

        //                }
        //            }
        //        }
        //    }
        //}
    }
}
