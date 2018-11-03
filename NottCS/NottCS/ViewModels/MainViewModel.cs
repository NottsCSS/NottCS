using System;
using System.Collections.Generic;
using System.Text;
using log4net;

namespace NottCS.ViewModels
{
    public class MainViewModel
    {
        private readonly ILog _logger;
        public MainViewModel(ILog logger)
        {
            _logger = logger;
            _logger.Info("MainViewModel created");
        }

    }
}
