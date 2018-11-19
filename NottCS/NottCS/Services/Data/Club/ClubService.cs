using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace NottCS.Services.Data.Club
{
    public class ClubService
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
    }
}
