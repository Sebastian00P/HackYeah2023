using HackYeahGWIZDapi.AppServices;
using HackYeahGWIZDapi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackYeahGWIZDapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictedEventController : ControllerBase
    {
        private readonly IPredictionPointApplicationService _predictionPointApplicationService;

        public PredictedEventController(IPredictionPointApplicationService predictionPointApplicationService)
        {
            _predictionPointApplicationService = predictionPointApplicationService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<List<PredictionEvent>> GetAll()
        {
            return await _predictionPointApplicationService.GetAll();
        }
    }
}
