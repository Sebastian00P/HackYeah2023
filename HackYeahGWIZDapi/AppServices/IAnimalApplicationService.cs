using HackYeahGWIZDapi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackYeahGWIZDapi.AppServices
{
    public interface IAnimalApplicationService
    {
        Task<List<Animal>> GetAll();
        Task<Animal> GetAnimalById(long Id);
    }
}