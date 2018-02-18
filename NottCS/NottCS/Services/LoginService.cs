using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using NottCS.Services.REST;

namespace NottCS.Services
{
    public static class LoginService
    {
        public static async Task<bool> AuthenticateWithCacheAsync()
        {
            try
            {
                AuthenticationResult ar = await App.ClientApplication.AcquireTokenSilentAsync(App.Scopes,
                    App.ClientApplication.Users.FirstOrDefault());
                //                RefreshUserData(ar.AccessToken);
                BaseRestService.SetupClient(ar.AccessToken);
                App.MicrosoftAuthenticationResult = ar;
                BaseRestService.SetupClient(ar.AccessToken);
            }
            catch (MsalException ex)
            {
                if (ex.ErrorCode == "user_interaction_required")
                {
                    Debug.WriteLine(ex.ErrorCode);
                    Debug.WriteLine("Something wrong with refresh token. Login required.");
                    return false;
                }
                else if (ex.ErrorCode == "user_null")
                {
                    Debug.WriteLine(ex.ErrorCode);
                    Debug.WriteLine("No users found on local cache. Login required.");
                    return false;
                }
                else
                {
                    Debug.WriteLine($"Unknown MsalException: {ex.Message}");
                    Debug.WriteLine($"Error code: {ex.ErrorCode}");
                    Debug.WriteLine($"Target site: {ex.TargetSite}");
                    Debug.WriteLine($"Please report this error to developers");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unknown generic exception occured: {ex.GetType()}");
                Debug.WriteLine($"Message: {ex.Message}");
                Debug.WriteLine($"Target site: {ex.TargetSite}");
                Debug.WriteLine($"Please report this error to developers");
                return false;
            }

            return true;

        }
        [Obsolete("Authenticate is deprecated, use AuthenticateWIthUIAsync or AuthenticateWithCacheAsync instead")]
        public static async Task Authenticate()
        {
            try
            {
                AuthenticationResult ar = await App.ClientApplication.AcquireTokenSilentAsync(App.Scopes,
                    App.ClientApplication.Users.FirstOrDefault());
                BaseRestService.SetupClient(ar.AccessToken);

                App.MicrosoftAuthenticationResult = ar;
                BaseRestService.SetupClient(ar.AccessToken);
            }
            catch (MsalException ex)
            {
                if (ex.ErrorCode == "user_interaction_required")
                {
                    Debug.WriteLine(ex.ErrorCode);
                    await AuthenticateWithUIAsync();
                }
                else if (ex.ErrorCode == "user_null")
                {
                    Debug.WriteLine(ex.ErrorCode);
                    await AuthenticateWithUIAsync();
                }
                else
                {
                    Debug.WriteLine($"Unknown MsalException: {ex.Message}");
                    Debug.WriteLine($"Error code: {ex.ErrorCode}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unknown  Exception occured: {ex.Message}");
                await AuthenticateWithUIAsync();
            }

        }

        public static Task SignOut()
        {
            foreach (var user in App.ClientApplication.Users)
            {
                App.ClientApplication.Remove(user);
            }

            return Task.FromResult(false);
        }

        public static async Task AuthenticateWithUIAsync()
        {
            AuthenticationResult ar = null;
            try
            {
                ar = await App.ClientApplication.AcquireTokenAsync(App.Scopes, App.UiParent);
                Debug.WriteLine(ar);
            }
            catch (MsalException ex)
            {
                //TODO: check appropriate error codes and do appropriate stuff, currently it only prints to Debug output

                if (ex.ErrorCode == "access_denied")
                {
                    Debug.WriteLine("Authentication cancelled");
                }
                else if (ex.ErrorCode == "authentication_ui_failed")
                {
                    Debug.WriteLine("Authentication UI closed");
                }
                else
                {
                    Debug.WriteLine($"Unknown MsalException: {ex.Message}");
                    Debug.WriteLine($"Error code: {ex.ErrorCode}");
                    Debug.WriteLine($"Target site: {ex.TargetSite}");
                    Debug.WriteLine($"Please report this error to developers");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unknown generic exception occured: {ex.GetType()}");
                Debug.WriteLine($"Message: {ex.Message}");
                Debug.WriteLine($"Target site: {ex.TargetSite}");
                Debug.WriteLine($"Please report this error to developers");
            }

            if (ar == null)
            {
                Debug.WriteLine("Null authentication result.");
                return;
            }
            await Task.Run(()=>BaseRestService.SetupClient(ar.AccessToken));
            Debug.WriteLine($"time limit: {ar.ExpiresOn}");
            App.MicrosoftAuthenticationResult = ar;
        }
    }
}
