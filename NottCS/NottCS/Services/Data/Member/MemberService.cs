using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NottCS.Models;
using NottCS.Services.Data.Club;

namespace NottCS.Services.Data.Member
{
    public class MemberService : IMemberService
    {
        private readonly BackendService.BackendService _backendService;
        private readonly LocalDatabaseConnection _localDatabaseService;
        private readonly ILogger<MemberService> _logger;

        public MemberService(BackendService.BackendService backendService, LocalDatabaseConnection localDatabaseService, ILogger<MemberService> logger)
        {
            _backendService = backendService;
            _localDatabaseService = localDatabaseService;
            _logger = logger;
        }

        public Task<List<Models.Member>> GetAllMembersAsync()
        {
            return _localDatabaseService.Table<Models.Member>().OrderBy(s => s.Id).ToListAsync();
        }

        public Task<List<Models.Member>> GetMemberByUserId(int userId)
        {
            return _localDatabaseService.Table<Models.Member>().Where(s => s.UserId == userId).ToListAsync();
        }

        public Task<List<Models.Member>> GetMemberByStatusAsync(MemberStatus status)
        {
            throw new NotImplementedException();
        }

        public Task<List<Models.Member>> GetMemberByClubIdAsync(int clubId)
        {
            throw new NotImplementedException();
        }

        public Task AddMemberAsync(Models.Member member)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMemberAsync(Models.Member member)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMemberAsync(Models.Member member)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMemberAsync(int memberId)
        {
            throw new NotImplementedException();
        }
    }
}
