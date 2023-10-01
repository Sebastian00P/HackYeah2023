using HackYeahGWIZDapi.AppContext;
using HackYeahGWIZDapi.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackYeahGWIZDapi.AppServices
{
    public class PredictionPointApplicationService : IPredictionPointApplicationService
    {
        private readonly ApplicationContext _context;

        public PredictionPointApplicationService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task Create(PredictionEvent predictionEvent)
        {
            await _context.PredictionEvents.AddAsync(predictionEvent);
            await _context.SaveChangesAsync();
        }

        public async Task<List<PredictionEvent>> GetAll()
        {
            return await _context.PredictionEvents.ToListAsync();
        }
    }
}
