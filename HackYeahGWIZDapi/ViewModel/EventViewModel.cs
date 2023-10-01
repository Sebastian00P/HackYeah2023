using HackYeahGWIZDapi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackYeahGWIZDapi.ViewModel
{
    public class EventViewModel
    {
        public long? EventId { get; set; }
        public Localization Localization { get; set; }
        public User User { get; set; }
        public EventPhoto EventPhotos { get; set; }
        public long AnimalId { get; set; }
        public string Date { get; set; }
        public string ExpiredTime { get; set; }
    }
}
