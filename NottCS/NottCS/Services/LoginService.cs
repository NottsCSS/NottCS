using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
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
