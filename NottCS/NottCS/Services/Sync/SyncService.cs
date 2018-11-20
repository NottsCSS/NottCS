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
        private readonly ILogger<ClubService> _logger;

        public SyncService(BackendService.BackendService backendService, ILogger<ClubService> logger)
        {
            _backendService = backendService;
            _logger = logger;
        }

        public async Task Sync()
        {
            var userTask = SyncUser();
            var memberTask = SyncMember();
            var clubTask = SyncClub();
            
            await userTask;
            await memberTask;
            await clubTask;
        }

        private async Task SyncClub()
        {
            var conn = new LocalDatabaseConnection();
            var clubList = await _backendService.RequestGetListAsync<ClubList>();
            await conn.ExecuteAsync("DELETE FROM Club");
            foreach (var item in clubList.Results)
            {
                await conn.InsertOrReplaceAsync(item);
            }
        }

        private async Task SyncUser()
        {
            var conn = new LocalDatabaseConnection();
            var user = await _backendService.GetUserAsync();
            await conn.ExecuteAsync("DELETE FROM User");
            await conn.InsertOrReplaceAsync(user);
        }
        private async Task SyncMember()
        {
            var conn = new LocalDatabaseConnection();
            var memberList = await _backendService.RequestGetListAsync<MemberList>();
            await conn.ExecuteAsync("DELETE FROM Member");
            foreach (var item in memberList.Results)
            {
                await conn.InsertOrReplaceAsync(item);
            }
        }
    }
}
