using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NottCS.Services.Data;
using NottCS.Services.Data.Club;
using NottCS.Models;

namespace NottCS.Services.Sync
{
    public class SyncService
    {
        private readonly BackendService.BackendService _backendService;
        private readonly LocalDatabaseConnection _localDatabaseService;
        private readonly ILogger<ClubService> _logger;

        public SyncService(BackendService.BackendService backendService, LocalDatabaseConnection localDatabaseService, ILogger<ClubService> logger)
        {
            _backendService = backendService;
            _localDatabaseService = localDatabaseService;
            _logger = logger;
        }

        public async Task Sync()
        {
            var clubList = await _backendService.GetClubsAsync();
            await _localDatabaseService.ExecuteAsync("DELETE FROM Club");
            foreach (var item in clubList.Results)
            {
                await _localDatabaseService.InsertOrReplaceAsync(item);
            }
        }
    }
}
