using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Extensions.Logging;
using NottCS.Models;
using NottCS.Services.BackendService;
using NottCS.Services.LoginService;
using NottCS.Services.Navigation;
using NottCS.ViewModels.Test;
using Xamarin.Forms;
using ILogger = NLog.ILogger;

namespace NottCS.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        private readonly ILogger<MenuViewModel> _logger;
        private readonly INavigationService _navigationService;
        private readonly ILoginService _loginService;
        private readonly BackendService _backendService;

        public MenuViewModel(ILogger<MenuViewModel> logger, INavigationService navigationService, 
            ILoginService loginService, BackendService backendService)
        {
            _logger = logger;
            _navigationService = navigationService;
            _loginService = loginService;
            _backendService = backendService;
            _logger.LogInformation("MenuViewModel created");
        }
        public List<HomeMenuItem> MenuItems { get; set; } = new List<HomeMenuItem>()
        {
            new HomeMenuItem(){ImageUri = "xamarin_logo.png", Name="Home", ViewModelType = typeof(HomeViewModel)},
            new HomeMenuItem(){ImageUri = "account_box_icon.png", Name="Profile", ViewModelType = typeof(ProfileViewModel)},
            new HomeMenuItem(){ImageUri = "account_box_icon.png", Name="DITest", ViewModelType = typeof(DITestViewModel)},
            new HomeMenuItem(){ImageUri = "xamarin_logo.png", Name="DbTest", ViewModelType = typeof(DatabaseTestViewModel)}
        };

        public ICommand NavigateCommand => new Command<object>(async(t) => await Navigate(t));
        public ICommand SignOutCommand => new Command(async () => await SignOut());

        private async Task Navigate(object param)
        {
            if (!(param is HomeMenuItem item))
            {
                _logger.LogError($"Parameter passed to NavigateCommand is not of type HomeMenuItem, is of type: {param.GetType()}");
                return;
            }
            _logger.LogDebug("NavigateCommand called");
            await _navigationService.SetDetailPageAsync(item.ViewModelType);
        }

        private async Task SignOut()
        {
            try
            {
                await _loginService.SignOutAsync();
                _backendService.ResetClient();
                await _navigationService.SetMainPageAsync<LoginViewModel>();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
            }
        }
    }
}
