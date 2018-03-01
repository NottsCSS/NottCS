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
    internal static class StartupService
    {
        internal static async Task InitializeAsync()
        {
            bool canAuthenticate = await LoginService.MicrosoftAuthenticateWithCacheAsync();
            DebugService.WriteLine($"Can authenticate with cached data: {canAuthenticate}");
            Stopwatch stopwatch = new Stopwatch();

            if (canAuthenticate)
            {
                var userData = await RestService.RequestGetAsync<User>();
                if (userData.Item1 == "OK") //first item represents whether the request is successful
                {
                    //if either studentId or librarynumber is not filled that means is new user
                    if (String.IsNullOrEmpty(userData.Item2.StudentId) ||
                        String.IsNullOrEmpty(userData.Item2.LibraryNumber))
                    {
                        stopwatch.Start();
                        await NavigationService.NavigateToAsync<RegistrationViewModel>(userData.Item2);
                        DebugService.WriteLine($"Navigation took {stopwatch.ElapsedMilliseconds}ms");
                    }
                    else
                    {
                        stopwatch.Start();
                        await NavigationService.NavigateToAsync<AccountViewModel>(userData.Item2);
                        DebugService.WriteLine($"Navigation took {stopwatch.ElapsedMilliseconds}ms");
                    }
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
        }
    }
}
