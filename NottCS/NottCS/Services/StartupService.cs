using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using NottCS.Models;
using NottCS.ViewModels;
using NottCS.Services;
using NottCS.Services.Navigation;
using NottCS.Services.REST;

namespace NottCS.Services
{

    //TODO: split up initialize async to allow more modular code such that loginViewModel can call to check registration
    internal static class StartupService
    {
        internal static async Task InitializeAsync()
        {
            //Loading Dialog
            var loadingDialog = Acr.UserDialogs.UserDialogs.Instance.Loading("Beep beep bop...");
            loadingDialog.Show();

            bool canAuthenticate = await LoginService.MicrosoftAuthenticateWithCacheAsync();
            DebugService.WriteLine($"Can authenticate with cached data: {canAuthenticate}");
            Stopwatch stopwatch = new Stopwatch();

            if (canAuthenticate)
            {
                var userData = await RestService.RequestGetAsync<User>();
                if (userData.Item1 == "OK") //first item represents whether the request is successful
                {
                    bool isNewStudent = String.IsNullOrEmpty(userData.Item2.StudentId) ||
                                        String.IsNullOrEmpty(userData.Item2.LibraryNumber) ||
                                        String.IsNullOrEmpty(userData.Item2.Course);

                    Debug.WriteLine($"Can Authenticate {isNewStudent}");

                    //if either studentId or librarynumber is not filled that means is new user
                    if (String.IsNullOrEmpty(userData.Item2.StudentId) ||
                        String.IsNullOrEmpty(userData.Item2.LibraryNumber) ||
                        String.IsNullOrEmpty(userData.Item2.Course))
                    {
                        stopwatch.Start();
                        Debug.WriteLine("Registration required");
                        await NavigationService.NavigateToAsync<RegistrationViewModel>(userData.Item2);
                        DebugService.WriteLine($"Navigation took {stopwatch.ElapsedMilliseconds}ms");
                    }
                    else
                    {
                        stopwatch.Start();
                        await NavigationService.NavigateToAsync<HomeViewModel>(userData.Item2);
                        DebugService.WriteLine($"Navigation took {stopwatch.ElapsedMilliseconds}ms");
                    }
//                    await NavigationService.NavigateToAsync<HomeViewModel>(userData.Item2);
                }
                else
                {
                    stopwatch.Start();
                    await NavigationService.NavigateToAsync<LoginViewModel>();
                    DebugService.WriteLine($"Navigation took {stopwatch.ElapsedMilliseconds}ms");
                }
            }
            else
            {
                stopwatch.Start();
                await NavigationService.NavigateToAsync<LoginViewModel>();
                DebugService.WriteLine($"Navigation took {stopwatch.ElapsedMilliseconds}ms");
            }

            loadingDialog.Hide();
        }
    }
}
