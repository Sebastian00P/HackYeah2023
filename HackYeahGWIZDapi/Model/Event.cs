using System;
using System.ComponentModel.DataAnnotations;

namespace HackYeahGWIZDapi.Model
{
    public class Event
    {
        [Key]
        public long EventId { get; set; }
        public virtual Localization Localization { get; set; }
        public virtual User User { get; set; }
        public virtual EventPhoto EventPhotos { get; set; }
        public virtual Animal Animal { get; set; }
        public DateTime Date { get; set; }
        public DateTime ExpiredTime { get; set; }

    }
}
