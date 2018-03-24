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
        private async Task SignInAsync()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                Debug.WriteLine($"IsBusy: {IsBusy}");                                                                                                                                                                                       
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                //error dialog handled inside SignInMicrosoftAsync, so no need to handle here
                
                bool canAuthenticateMicrosoft = await LoginService.SignInMicrosoftAsync(); 
                if (canAuthenticateMicrosoft)
                {
                    await LoginService.SignInBackendAndNavigateAsync();
                }

                //Debugging code
                DebugService.WriteLine("Sign in function called");

                IsBusy = false;
            }
            
        }
    }
}