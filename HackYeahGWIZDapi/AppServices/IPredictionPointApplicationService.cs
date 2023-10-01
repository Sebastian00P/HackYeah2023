using HackYeahGWIZDapi.Model;
using System.Threading.Tasks;

namespace HackYeahGWIZDapi.AppServices
{
    public interface IPredictionPointApplicationService
    {
        Task Create(PredictionEvent predictionEvent);
    }
}