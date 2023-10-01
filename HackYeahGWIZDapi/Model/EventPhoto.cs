using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;

namespace HackYeahGWIZDapi.Model
{
    public class EventPhoto
    {
        [Key]
        public long? PhotoId { get; set; }
        public string Image { get; set; }

    }
}
