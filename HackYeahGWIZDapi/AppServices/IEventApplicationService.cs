using HackYeahGWIZDapi.Model;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using HackYeahGWIZDapi.ViewModel;

namespace HackYeahGWIZDapi.AppServices
{
    public interface IEventApplicationService
    {
        Task Create(EventViewModel _event);
        Task<List<EventViewModel>> GetAll();
        Task<List<Event>> GetAllNotExpired();
        Task UpdateExpiredTime(Event _event, Event _secondEvent);
    }
}