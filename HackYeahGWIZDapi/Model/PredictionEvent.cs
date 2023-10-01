using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HackYeahGWIZDapi.Model
{
    public class PredictionEvent
    {
        [Key]
        public long PredictionId { get; set; }
        public Localization Localization { get; set; }
        public EventPhoto Photo { get; set; }
    }
}
