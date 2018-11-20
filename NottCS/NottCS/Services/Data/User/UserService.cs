using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NottCS.Services.Data.User.NottCS.Services.Data.Club;

namespace NottCS.Services.Data.User
{
    public class UserService : IUserService
    {
        private readonly BackendService.BackendService _backendService;
        private readonly LocalDatabaseConnection _localDatabaseService;
        private readonly ILogger<UserService> _logger;
        public UserService(BackendService.BackendService backendService, LocalDatabaseConnection localDatabaseService, ILogger<UserService> logger)
        {
            _backendService = backendService;
            _localDatabaseService = localDatabaseService;
            _logger = logger;
        }
        public Task<Models.User> GetUser()
        {
            return _localDatabaseService.Table<Models.User>().FirstOrDefaultAsync();
        }

        public Task UpdateUser(Models.User user)
        {
            throw new NotImplementedException();
        }
    }
}
