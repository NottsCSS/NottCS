using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using NottCS.Models;
using NottCS.Services.Navigation;
using NottCS.Services.REST;
using NottCS.ViewModels;

namespace NottCS.Services
{
    public static class LoginService
    {
        public static async Task<bool> MicrosoftAuthenticateWithCacheAsync()
        {
            try
            {
                AuthenticationResult ar = await App.ClientApplication.AcquireTokenSilentAsync(App.Scopes,
                    App.ClientApplication.Users.FirstOrDefault());
                //                RefreshUserData(ar.AccessToken);
                RestService.SetupClient(ar.AccessToken);
                App.MicrosoftAuthenticationResult = ar;
                DebugService.WriteLine($"{ar.User.Name} successfully authenticated with microsoft server");
                DebugService.WriteLine($"Token expires on: {ar.ExpiresOn}");
                DebugService.WriteLine(ar.AccessToken);
            }
            catch (MsalUiRequiredException ex)
            {
                if (ex.ErrorCode == MsalUiRequiredException.UserNullError)
                {
                    DebugService.WriteLine(ex.ErrorCode);
                    DebugService.WriteLine("Null user was passed (no user found on local cache). Login required.");
                    return false;
                }
                else
                {
                    DebugService.WriteLine($"MsalUiRequiredException: {ex.Message}");
                    DebugService.WriteLine($"Error code: {ex.ErrorCode}");
                    DebugService.WriteLine($"Target site: {ex.TargetSite}");
                    return false;
                }
            }
            catch (MsalServiceException ex)
            {
                DebugService.WriteLine($"MsalServiceException thrown");
                DebugService.WriteLine($"Error code: {ex.ErrorCode}");
            }

            catch (MsalException ex)
            {
                DebugService.WriteLine($"Other MsalException thrown");
                DebugService.WriteLine($"Error code: {ex.ErrorCode}");
            }
            catch (Exception ex)
            {
                DebugService.WriteLine($"Unknown generic exception occured: {ex.GetType()}");
                DebugService.WriteLine($"Message: {ex.Message}");
                DebugService.WriteLine($"Target site: {ex.TargetSite}");
                DebugService.WriteLine($"Please report this error to developers");
                return false;
            }

            return true;

        }

        public static async Task SignOut()
        {
            foreach (var user in App.ClientApplication.Users)
            {
                App.ClientApplication.Remove(user);
                RestService.ResetClient();
            }
            NavigationService.ClearNavigation();
            await NavigationService.NavigateToAsync<LoginViewModel>();
            
        }

        public static async Task SignInMain()
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
                        String.IsNullOrEmpty(userData.Item2.LibraryNumber) ||
                        String.IsNullOrEmpty(userData.Item2.Course))
                    {
                        stopwatch.Start();
                        await NavigationService.NavigateToAsync<RegistrationViewModel>(userData.Item2);
                        DebugService.WriteLine($"Navigation took {stopwatch.ElapsedMilliseconds}ms");
                    }
                    else
                    {
                        stopwatch.Start();
                        await NavigationService.NavigateToAsync<HomeViewModel>(userData.Item2);
                        DebugService.WriteLine($"Navigation took {stopwatch.ElapsedMilliseconds}ms");
                    }
                    await NavigationService.NavigateToAsync<HomeViewModel>(userData.Item2);
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

        public static async Task SignInBackend()
        {
            Stopwatch stopwatch = new Stopwatch();

            var userData = await RestService.RequestGetAsync<User>();
            if (userData.Item1 == "OK") //first item represents whether the request is successful
            {
                //if either studentId or librarynumber is not filled that means is new user
                if (String.IsNullOrEmpty(userData.Item2.StudentId) ||
                    String.IsNullOrEmpty(userData.Item2.LibraryNumber) ||
                    String.IsNullOrEmpty(userData.Item2.Course))
                {
                    stopwatch.Start();
                    await NavigationService.NavigateToAsync<RegistrationViewModel>(userData.Item2);
                    DebugService.WriteLine($"Navigation took {stopwatch.ElapsedMilliseconds}ms");
                }
                else
                {
                    stopwatch.Start();
                    await NavigationService.NavigateToAsync<HomeViewModel>(userData.Item2);
                    DebugService.WriteLine($"Navigation took {stopwatch.ElapsedMilliseconds}ms");
                }
                await NavigationService.NavigateToAsync<HomeViewModel>(userData.Item2);
            }
            else
            {
                stopwatch.Start();
                await NavigationService.NavigateToAsync<LoginViewModel>();
                DebugService.WriteLine($"Navigation took {stopwatch.ElapsedMilliseconds}ms");
            }
        }

        public static async Task MicrosoftAuthenticateWithUIAsync()
        {
            AuthenticationResult ar = null;
            try
            {
                ar = await App.ClientApplication.AcquireTokenAsync(App.Scopes, App.UiParent);
                DebugService.WriteLine(ar);
            }
            catch (MsalException ex)
            {
                //TODO: check appropriate error codes and do appropriate stuff, currently it only prints to Debug output

                if (ex.ErrorCode == "access_denied")
                {
                    DebugService.WriteLine("Authentication cancelled");
                }
                else if (ex.ErrorCode == "authentication_ui_failed")
                {
                    DebugService.WriteLine("Authentication UI closed");
                }
                else
                {
                    DebugService.WriteLine($"Unknown MsalException: {ex.Message}");
                    DebugService.WriteLine($"Error code: {ex.ErrorCode}");
                    DebugService.WriteLine($"Target site: {ex.TargetSite}");
                    DebugService.WriteLine($"Please report this error to developers");
                }
            }
            catch (Exception ex)
            {
                DebugService.WriteLine($"Unknown generic exception occured: {ex.GetType()}");
                DebugService.WriteLine($"Message: {ex.Message}");
                DebugService.WriteLine($"Target site: {ex.TargetSite}");
                DebugService.WriteLine($"Please report this error to developers");
            }

            if (ar == null)
            {
                DebugService.WriteLine("Null authentication result.");
                return;
            }
            RestService.SetupClient(ar.AccessToken);
            DebugService.WriteLine($"time limit: {ar.ExpiresOn}");
            App.MicrosoftAuthenticationResult = ar;
            DebugService.WriteLine($"{ar.User.Name} successfully authenticated with microsoft server");
            DebugService.WriteLine(ar.AccessToken);

        }

        public static Task BackendAuthenticateAsync()
        {
            return Task.FromResult(false);
        }
    }
}
