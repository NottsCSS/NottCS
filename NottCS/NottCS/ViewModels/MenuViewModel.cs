using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using log4net;
using NottCS.Models;
using Xamarin.Forms;

namespace NottCS.ViewModels
{
    public class MenuViewModel
    {
        private readonly ILog _logger;

        public MenuViewModel(ILog logger)
        {
            _logger = logger;
            _logger.Info("MenuViewModel created");
        }
        public List<HomeMenuItem> MenuItems { get; set; } = new List<HomeMenuItem>()
        {
            new HomeMenuItem(){ImageUri = "xamarin_logo.png", Name="Home", ViewModelType = typeof(AboutViewModel)},
            new HomeMenuItem(){ImageUri = "account_box_icon.png", Name="About", ViewModelType = typeof(AboutViewModel)}
        };

        public ICommand NavigateCommand => new Command();
        private 
    }
}
