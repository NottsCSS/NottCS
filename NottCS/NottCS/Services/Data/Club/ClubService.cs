using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NottCS.Models;
using NottCS.Services.Data.Member;
using NottCS.Services.Data.User.NottCS.Services.Data.Club;

namespace NottCS.Services.Data.Club
{
    //Will have data race problem, not sure if the use case will fully show it
    public class ClubService : IClubService
    {
        private readonly BackendService.BackendService _backendService;
        private readonly LocalDatabaseConnection _localDatabaseService;
        private readonly ILogger<ClubService> _logger;

        public ClubService(BackendService.BackendService backendService, LocalDatabaseConnection localDatabaseService, 
            ILogger<ClubService> logger, IMemberService memberService)
        {
            _backendService = backendService;
            _localDatabaseService = localDatabaseService;
            _logger = logger;
        }
        public Task<List<Models.Club>> GetAllClubsAsync()
        {
            return _localDatabaseService.QueryAsync<Models.Club>("SELECT * FROM Club ORDER BY Name COLLATE NOCASE ASC");
        }

        public async Task<List<Models.Club>> GetMyClubsAsync()
        {
            //This query checks for member status, remove the check if don't want
            var clubQuery =
                "Select Club.Id, Club.Name, Club.Description, Club.IconUrl, Club.CreatedTime, Club.UpdatedTime, Member.Id, Member.Status From Club " +
                "Left Join Member on Club.Id = Member.ClubId AND Member.Status = 1 " + 
                "Inner Join User on User.Id = Member.UserId " +
                "ORDER BY Member.Id";
            try
            {
                var a = await _localDatabaseService.QueryAsync<Models.Club>(clubQuery);
                return a;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return new List<Models.Club>();
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
