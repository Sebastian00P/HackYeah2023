using HackYeahGWIZDapi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackYeahGWIZDapi.AppServices
{
    public interface IPredictionPointApplicationService
    {
        Task Create(PredictionEvent predictionEvent);
        Task<List<PredictionEvent>> GetAll();
    }
}