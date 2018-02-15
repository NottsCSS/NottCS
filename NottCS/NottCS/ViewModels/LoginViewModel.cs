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

namespace NottCS.ViewModels
{
    internal class LoginViewModel : BaseViewModel
    {

        private string _loginMessage = "Some Login Message";
        
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
            IsBusy = true;

            LoginMessage = await LoginService.Authenticate();
            var a = App.MicrosoftAuthenticationResult.User.DisplayableId;
            Debug.WriteLine($"DisplayableID: {a}");
            Debug.WriteLine($"Identifier: {App.MicrosoftAuthenticationResult.User.Identifier}");
            Debug.WriteLine($"Identity Provider: {App.MicrosoftAuthenticationResult.User.IdentityProvider}");
            Debug.WriteLine($"Name: {App.MicrosoftAuthenticationResult.User.Name}");

            if (!string.IsNullOrEmpty(LoginMessage))
            {
                await NavigationService.NavigateToAsync<AccountViewModel>(a);
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