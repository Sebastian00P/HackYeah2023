using HackYeahGWIZDapi.AppServices;
using HackYeahGWIZDapi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackYeahGWIZDapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalApplicationService _animalApplicationService;

        public AnimalController(IAnimalApplicationService animalApplicationService)
        {
            _animalApplicationService = animalApplicationService;
        }

        [HttpGet]
        [Route("GetAnimalById")]
        public async Task<Animal> GetAnimalById(long id)
        {
            return await _animalApplicationService.GetAnimalById(id);
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<List<Animal>> GetAll()
        {
            return await _animalApplicationService.GetAll();
        }
    }
}
