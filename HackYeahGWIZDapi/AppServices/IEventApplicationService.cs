using HackYeahGWIZDapi.Model;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using HackYeahGWIZDapi.ViewModel;

namespace HackYeahGWIZDapi.AppServices
{
    public interface IEventApplicationService
    {
        Task Create(Event _event);
        Task<List<EventViewModel>> GetAll();
    }
}