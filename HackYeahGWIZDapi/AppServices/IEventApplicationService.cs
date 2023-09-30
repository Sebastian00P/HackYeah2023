using HackYeahGWIZDapi.Model;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace HackYeahGWIZDapi.AppServices
{
    public interface IEventApplicationService
    {
        Task Create(Event _event);
        Task<List<Event>> GetAll();
    }
}