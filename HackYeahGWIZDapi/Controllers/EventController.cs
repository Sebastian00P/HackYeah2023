using HackYeahGWIZDapi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HackYeahGWIZDapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        [HttpGet]
        public List<Event> GetAll()
        {
            return new List<Event>();
        }
    }
}
