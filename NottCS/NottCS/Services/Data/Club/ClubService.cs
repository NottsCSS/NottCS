using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NottCS.Models;

namespace NottCS.Services.Data.Club
{
    public class ClubService : IClubService
    {
        private readonly BackendService.BackendService _backendService;
        private readonly LocalDatabaseService _localDatabaseService;
        private readonly ILogger<ClubService> _logger;

        public ClubService(BackendService.BackendService backendService, LocalDatabaseService localDatabaseService, ILogger<ClubService> logger)
        {
            _backendService = backendService;
            _localDatabaseService = localDatabaseService;
            _logger = logger;
        }
        public Task<List<Models.Club>> GetAllClubsAsync()
        {
            return _localDatabaseService.Table<Models.Club>().ToListAsync();
        }

        public Task<Models.Club> GetClubByIdAsync(int id)
        {
            return _localDatabaseService.Table<Models.Club>().FirstOrDefaultAsync(i => i.Id == id);
        }

        public Task<Models.Club> GetClubByNameAsync(string name)
        {
            return _localDatabaseService.Table<Models.Club>().FirstOrDefaultAsync(i => i.Name.ToUpperInvariant() == name.ToUpperInvariant());
        }

        public async Task AddClubAsync(Models.Club club)
        {
            var remoteTask = _backendService.AddClubAsync(club);
            await remoteTask;
            await _localDatabaseService.InsertOrReplaceAsync(club);
        }

        public Task DeleteClubAsync(Models.Club club)
        {
            throw new NotImplementedException();
        }

        public Task DeleteClubAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
