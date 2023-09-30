using HackYeahGWIZDapi.AppServices;
using HackYeahGWIZDapi.Model;
using HackYeahGWIZDapi.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackYeahGWIZDapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventApplicationService _eventApplicationService;
        public EventController(IEventApplicationService eventApplicationService)
        {
            _eventApplicationService = eventApplicationService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<EventViewModel>> GetAll()
        {
            return await _eventApplicationService.GetAll();
        }
        [HttpPost]
        [Route("Create")]
        public async Task Create(Event _event)
        {
            await _eventApplicationService.Create(_event);
        }
        
    }
}
