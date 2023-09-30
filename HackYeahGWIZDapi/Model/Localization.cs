using System.ComponentModel.DataAnnotations;

namespace HackYeahGWIZDapi.Model
{
    public class Localization
    {
        [Key]
        public long LocalizationId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
