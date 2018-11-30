using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NottCS.Services.Data.Event
{
    public interface IEventService
    {
        Task<List<Models.Event>> GetAllEventsAsync();
        Task<List<Models.Event>> GetEventByClubAsync(int clubid);
        Task<List<Models.Event>> GetEventByClubAsync(Models.Club club);
        Task<Models.Event> GetEventByIdAsync(int id);
        Task<Models.Event> GetEventByTitleAsync(string title);
        Task AddEventAsync(Models.Event Event);
        Task DeleteEventAsync(Models.Event Event);
        Task DeleteEventAsync(int id);
    }
}
