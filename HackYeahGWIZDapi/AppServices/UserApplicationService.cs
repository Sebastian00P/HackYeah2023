using HackYeahGWIZDapi.AppContext;
using HackYeahGWIZDapi.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackYeahGWIZDapi.AppServices
{
    public class UserApplicationService : IUserApplicationService
    {
        private readonly ApplicationContext _context;
        public UserApplicationService(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
