using System.ComponentModel.DataAnnotations;

namespace HackYeahGWIZDapi.Model
{
    public class Animal
    {
        [Key]
        public long AnimalId { get; set; }
        public string Name { get; set; }
    }
}
