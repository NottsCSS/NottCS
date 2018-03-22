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
                bool canSignInWithCache = await LoginService.MicrosoftAuthenticateWithCacheAsync();
                if (!canSignInWithCache)
                {
                    await LoginService.MicrosoftAuthenticateWithUIAsync();
                }

                DebugService.WriteLine($"Authentication took {stopwatch.ElapsedMilliseconds} ms");
                stopwatch.Restart();

                var getUserTask = Task.Run(() => RestService.RequestGetAsync<User>());
                var getUserTaskResult = await getUserTask;

                DebugService.WriteLine($"User request took {stopwatch.ElapsedMilliseconds} ms");
                stopwatch.Restart();

                if (getUserTaskResult.Item1 == "OK")
                {
                    NavigationService.ClearNavigation();
                    await NavigationService.NavigateToAsync<HomeViewModel>(getUserTaskResult.Item2);
                    DebugService.WriteLine($"Navigation took {stopwatch.ElapsedMilliseconds} ms");
                    stopwatch.Stop();
                }
                else
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert(getUserTaskResult.Item1, "Login Error", "Ok");
                }

                //Debugging code
                DebugService.WriteLine("Sign in function called");

                IsBusy = false;
            }
            
        }
    }
}