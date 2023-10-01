using System.Threading.Tasks;

namespace HackYeahGWIZDapi.AppModule
{
    public interface IJobApplicationService
    {
        Task CheckMultiplyEvents();
        Task PredictNextLocationForEventGroupsInCloseProximity();
    }
}