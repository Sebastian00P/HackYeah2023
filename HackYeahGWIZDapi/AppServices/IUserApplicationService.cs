using HackYeahGWIZDapi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackYeahGWIZDapi.AppServices
{
    public interface IUserApplicationService
    {
        Task<List<User>> GetAll();
    }
}