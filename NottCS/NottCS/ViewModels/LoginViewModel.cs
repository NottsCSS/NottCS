using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Extensions.Logging;
using NLog;
using NottCS.Services.BackendService;
using NottCS.Services.LoginService;
using NottCS.Services.Navigation;
using NottCS.Services.Sync;
using Xamarin.Forms;

namespace NottCS.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly ILogger<LoginViewModel> _logger;
        private readonly ILoginService _loginService;
        private readonly BackendService _backendService;
        private readonly SyncService _syncService;
        private readonly INavigationService _navigationService;
        private string _message;

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public LoginViewModel(ILogger<LoginViewModel> logger, ILoginService loginService, 
            BackendService backendService, SyncService syncService, INavigationService navigationService)
        {
            _logger = logger;
            _loginService = loginService;
            _backendService = backendService;
            _syncService = syncService;
            _navigationService = navigationService;
            logger.LogInformation("LoginViewModel created");
        }

        public ICommand SignInCommand => new Command(async() => await SignIn());
        public ICommand SignOutCommand => new Command(async () => await SignOut());
        public ICommand SyncCommand => new Command(async () => await Sync());

        private async Task SignIn()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                var result = await _loginService.SignInAsync();
                Message = result.AccessToken;
                _backendService.SetupClient(Message);
                var syncTask =  _syncService.Sync();
                await _navigationService.SetMainPageAsync<MainViewModel>();
                await syncTask;
            }
            catch (MicrosoftAccountException e)
            {
                Message = e.Message;
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.ToString());
                Message = $"type: {e.GetType()} message: {e.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }
        private async Task SignOut()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                await _loginService.SignOutAsync();
                _backendService.ResetClient();
            }
            catch (Exception e)
            {
                Message = e.ToString();
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task Sync()
        {
            await _syncService.Sync();
        }
    }
}
