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
        public static async Task<bool> MicrosoftAuthenticateWithCacheAsync()
        {
            try
            {
                AuthenticationResult ar = await App.ClientApplication.AcquireTokenSilentAsync(App.Scopes,
                    App.ClientApplication.Users.FirstOrDefault());
                //                RefreshUserData(ar.AccessToken);
                RestService.SetupClient(ar.AccessToken);
                App.MicrosoftAuthenticationResult = ar;
                Debug.WriteLine($"{ar.User.Name} successfully authenticated with microsoft server");
                Debug.WriteLine(ar.AccessToken);
            }
            catch (MsalUiRequiredException ex)
            {
                if (ex.ErrorCode == MsalUiRequiredException.UserNullError)
                {
                    Debug.WriteLine(ex.ErrorCode);
                    Debug.WriteLine("Null user was passed (no user found on local cache). Login required.");
                    return false;
                }
                else
                {
                    Debug.WriteLine($"MsalUiRequiredException: {ex.Message}");
                    Debug.WriteLine($"Error code: {ex.ErrorCode}");
                    Debug.WriteLine($"Target site: {ex.TargetSite}");
                    return false;
                }
            }
            catch (MsalServiceException ex)
            {
                Debug.WriteLine($"MsalServiceException thrown");
                Debug.WriteLine($"Error code: {ex.ErrorCode}");
            }

            catch (MsalException ex)
            {
                Debug.WriteLine($"Other MsalException thrown");
                Debug.WriteLine($"Error code: {ex.ErrorCode}");
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

        public static Task SignOut()
        {
            foreach (var user in App.ClientApplication.Users)
            {
                App.ClientApplication.Remove(user);
                RestService.ResetClient();
            }

            return Task.FromResult(false);
        }

        public static async Task MicrosoftAuthenticateWithUIAsync()
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
            RestService.SetupClient(ar.AccessToken);
            Debug.WriteLine($"time limit: {ar.ExpiresOn}");
            App.MicrosoftAuthenticationResult = ar;
            Debug.WriteLine($"{ar.User.Name} successfully authenticated with microsoft server");
            Debug.WriteLine(ar.AccessToken);

        }

        public static Task BackendAuthenticateAsync()
        {
            return Task.FromResult(false);
        }
    }
}
