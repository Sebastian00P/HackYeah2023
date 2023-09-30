using HackYeahGWIZDapi.AppContext;
using HackYeahGWIZDapi.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackYeahGWIZDapi.AppServices
{
    public class AnimalApplicationService : IAnimalApplicationService
    {
        private readonly ApplicationContext _context;
        public AnimalApplicationService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Animal> GetAnimalById(long Id)
        {
            return await _context.Animals.Where(x => x.AnimalId == Id).FirstOrDefaultAsync();
        }

        public async Task<List<Animal>> GetAll()
        {
            return await _context.Animals.ToListAsync();
        }
    }
}
