using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NottCS.Services.Data.Club;
using NottCS.Services.Data.Member;

namespace NottCS.Services.Data.Event
{
    class EventService:IEventService
    {
        private readonly BackendService.BackendService _backendService;
        private readonly LocalDatabaseConnection _localDatabaseService;
        private readonly ILogger<ClubService> _logger;

        public EventService(BackendService.BackendService backendService, LocalDatabaseConnection localDatabaseService,
            ILogger<ClubService> logger)
        {
            _backendService = backendService;
            _localDatabaseService = localDatabaseService;
            _logger = logger;
        }

        public Task<List<Models.Event>> GetAllEventsAsync()
        {
            return null;
        }
        public Task<List<Models.Event>> GetEventByClubAsync(int clubid)
        {
            return null;
        }
        public Task<List<Models.Event>> GetEventByClubAsync(Models.Club club)
        {
            return null;
        }
        public Task<Models.Event> GetEventByIdAsync(int id)
        {
            return _localDatabaseService.Table<Models.Event>().FirstOrDefaultAsync(i => i.Id == id);
        }
        public Task<Models.Event> GetEventByTitleAsync(string title)
        {
            return _localDatabaseService.Table<Models.Event>().FirstOrDefaultAsync(i => i.Title.ToUpperInvariant() == title.ToUpperInvariant());
        }
        public Task AddEventAsync(Models.Event Event)
        {
            return null;
        }
        public Task DeleteEventAsync(Models.Event Event)
        {
            throw new NotImplementedException();
        }
        public Task DeleteEventAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
