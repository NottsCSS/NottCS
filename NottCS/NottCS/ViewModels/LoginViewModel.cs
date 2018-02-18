using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using NottCS.Models;
using Xamarin.Forms;
using Newtonsoft.Json;
using NottCS.Services;
using NottCS.Validations;
using NottCS.Services.Navigation;
using NottCS.Services.REST;

namespace NottCS.ViewModels
{
    internal class LoginViewModel : BaseViewModel
    {

        private string _loginMessage = "";
        
        public string LoginMessage
        {
            get => _loginMessage;
            set => SetProperty(ref _loginMessage, value);
        }
        
        /// <summary>
        /// Constructor to initialise values of various fields
        /// </summary>
        public LoginViewModel()
        {
            Title = "NottCS Login";
        }
        /// <summary>
        /// Command wrapper for sign in function
        /// </summary>
        public ICommand SignInCommand => new Command(async () => await SignInAsync());
        /// <summary>
        /// Command wrapper for sign out function
        /// </summary>
        public ICommand SignOutCommand => new Command(async () => await SignOutAsync());
        private async Task SignInAsync()
        {
            var stopwatch = new Stopwatch();
            IsBusy = true;

            await LoginService.AuthenticateWithUIAsync();
            string a = App.MicrosoftAuthenticationResult?.User.DisplayableId;
            Debug.WriteLine($"DisplayableID: {a}");
            Debug.WriteLine($"Identifier: {App.MicrosoftAuthenticationResult?.User.Identifier}");
            Debug.WriteLine($"Identity Provider: {App.MicrosoftAuthenticationResult?.User.IdentityProvider}");
            Debug.WriteLine($"Name: {App.MicrosoftAuthenticationResult?.User.Name}");

            Debug.WriteLine($"Authentication took {stopwatch.ElapsedMilliseconds} ms");
            stopwatch.Restart();

            var getUserTask = Task.Run(() => BaseRestService.RequestGetAsync<User>());
            var getUserTaskResult = await getUserTask;

            Debug.WriteLine($"User request took {stopwatch.ElapsedMilliseconds} ms");
            stopwatch.Restart();

            if (getUserTaskResult.Item1)
            {
                await NavigationService.NavigateToAsync<AccountViewModel>(getUserTaskResult.Item2);
                Debug.WriteLine($"Navigation took {stopwatch.ElapsedMilliseconds} ms");
                stopwatch.Stop();
            }
            else
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert("Unable to log you in to the server. Please try again",
                    "Login Error", "Ok");
            }

            //Debugging code
            Debug.WriteLine("Sign in function called");

            IsBusy = false;
        }
        private async Task SignOutAsync()
        {
            IsBusy = true;
            await LoginService.SignOut();
            Debug.WriteLine("Sign out function called");
            LoginMessage = "Signed out successfully";
            IsBusy = false;
        }
    }
}