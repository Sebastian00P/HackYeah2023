using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HackYeahGWIZDapi.Model
{
    public class User
    {
        [Key]
        public long? UserId { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
    }
}
