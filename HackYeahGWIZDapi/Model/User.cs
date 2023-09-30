using System.ComponentModel.DataAnnotations;

namespace HackYeahGWIZDapi.Model
{
    public class User
    {
        [Key]
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int Phone { get; set; }
    }
}
