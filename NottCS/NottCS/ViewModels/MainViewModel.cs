using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using ILogger = NLog.ILogger;

namespace NottCS.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly ILogger<MainViewModel> _logger;
        public MainViewModel(ILogger<MainViewModel> logger)
        {
            _logger = logger;
            logger.LogInformation("MainViewModel created");
        }

    }
}
