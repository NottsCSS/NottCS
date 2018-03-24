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
        /// <summary>
        /// Signs out, and then navigates to login page
        /// </summary>
        /// <returns></returns>
        public static async Task SignOutAndNavigateAsync()
        {
            foreach (var user in App.ClientApplication.Users)
            {
                App.ClientApplication.Remove(user);
                RestService.ResetClient();
            }
            
            await NavigationService.NavigateToAsync<LoginViewModel>();

        }

        /// <summary>
        /// First step of MSAL sign in, determines if UI is required, exception handled later
        /// </summary>
        /// <returns></returns>
        private static async Task<AuthenticationResult> InternalSignInMicrosoft()
        {
            try
            {
                AuthenticationResult ar = await App.ClientApplication.AcquireTokenSilentAsync(App.Scopes,
                    App.ClientApplication.Users.FirstOrDefault());
                DebugService.WriteLine("Login using cached data successful");
                return ar;
            }
            catch (MsalUiRequiredException)
            {
                DebugService.WriteLine("UI required");
                AuthenticationResult ar = await App.ClientApplication.AcquireTokenAsync(App.Scopes, App.UiParent);
                return ar;
            }
        }

        /// <summary>
        /// Handles exceptions and result of AcquireToken methods of MSAL
        /// </summary>
        /// <returns>
        /// CanAuthenticateWithMicrosoft
        /// </returns>
        public static async Task<bool> SignInMicrosoftAsync()
        {
            try
            {
                AuthenticationResult ar = await InternalSignInMicrosoft();
                Task setupClientTask = Task.Run(() => RestService.SetupClient(ar.AccessToken));
                App.MicrosoftAuthenticationResult = ar;

                DebugService.WriteLine("Login to microsoft successful");
                DebugService.WriteLine($"Welcome {ar.User.Name}");
                DebugService.WriteLine($"Token expires on: {ar.ExpiresOn}");
                DebugService.WriteLine($"Access token: {ar.AccessToken}");

                await setupClientTask;
                return true;
            }
            catch (MsalException msalException)
            {
                switch (msalException.ErrorCode)
                {
                    case MsalClientException.AuthenticationCanceledError:
                        Acr.UserDialogs.UserDialogs.Instance.Alert("Authentication cancelled",
                            "Microsoft Authentication Client Error", "OK");
                        break;
                    case MsalClientException.AuthenticationUiFailedError:
                        Acr.UserDialogs.UserDialogs.Instance.Alert("Authentication UI failed",
                            "Microsoft Authentication Client Error", "OK");
                        break;
                    case MsalClientException.DuplicateQueryParameterError:
                        Acr.UserDialogs.UserDialogs.Instance.Alert("Duplicate query parameter",
                            "Microsoft Authentication Client Error", "OK");
                        break;
                    case MsalClientException.InvalidJwtError:
                        Acr.UserDialogs.UserDialogs.Instance.Alert("Invalid JSON web token",
                            "Microsoft Authentication Client Error", "OK");
                        break;
                    case MsalClientException.JsonParseError:
                        Acr.UserDialogs.UserDialogs.Instance.Alert("JSON parse error",
                            "Microsoft Authentication Client Error", "OK");
                        break;
                    case MsalClientException.MultipleTokensMatchedError:
                        Acr.UserDialogs.UserDialogs.Instance.Alert("Multiple tokens matched",
                            "Microsoft Authentication Client Error", "OK");
                        break;
                    case MsalClientException.NetworkNotAvailableError:
                        Acr.UserDialogs.UserDialogs.Instance.Alert("Network not available",
                            "Microsoft Authentication Client Error", "OK");
                        break;
                    case MsalClientException.NonHttpsRedirectNotSupported:
                        Acr.UserDialogs.UserDialogs.Instance.Alert("Https redirect not supported",
                            "Microsoft Authentication Client Error", "OK");
                        break;
                    case MsalClientException.StateMismatchError:
                        Acr.UserDialogs.UserDialogs.Instance.Alert("State mismatch error",
                            "Microsoft Authentication Client Error", "OK");
                        break;
                    case MsalClientException.TenantDiscoveryFailedError:
                        Acr.UserDialogs.UserDialogs.Instance.Alert("Tenant discovery failed",
                            "Microsoft Authentication Client Error", "OK");
                        break;
                    case MsalServiceException.RequestTimeout:
                        Acr.UserDialogs.UserDialogs.Instance.Alert("Request timeout",
                            "Microsoft Authentication Service Error", "OK");
                        break;
                    case MsalServiceException.ServiceNotAvailable:
                        Acr.UserDialogs.UserDialogs.Instance.Alert("Service unavailable",
                            "Microsoft Authentication Service Error", "OK");
                        break;
                    case "access_denied":
                        Acr.UserDialogs.UserDialogs.Instance.Alert("Access denied", "Microsoft Authentication Error",
                            "OK");
                        break;
                    default:
                        Acr.UserDialogs.UserDialogs.Instance.Alert("Unknown error occured",
                            "Microsoft Authentication Error", "OK");
                        DebugService.WriteLine($"{msalException}");
                        break;
                }
            }
            catch (HttpRequestException httpException)
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert($"Http request error: {httpException}");
            }
            catch (Exception e)
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert($"Non-msal exception thrown by MSAL, error: {e}");
            }

            return false;
        }

        /// <summary>
        /// Connects to backend and handles data from backend
        /// </summary>
        /// <returns></returns>
        public static async Task SignInBackendAndNavigateAsync()
        {
            var userData = await RestService.RequestGetAsync<User>();
            if (userData.Item1 == "OK") //first item represents whether the request is successful
            {
                //if either studentId or librarynumber is not filled that means is new user
                if (String.IsNullOrEmpty(userData.Item2.StudentId) ||
                    String.IsNullOrEmpty(userData.Item2.LibraryNumber) ||
                    String.IsNullOrEmpty(userData.Item2.Course))
                {
                    DebugService.WriteLine("User is not registered");
                    await NavigationService.NavigateToAsync<RegistrationViewModel>(userData.Item2);
                }
                else
                {
                    DebugService.WriteLine("User is registered");
                    await NavigationService.NavigateToAsync<HomeViewModel>(userData.Item2);
                }
            }
            else
            {
                DebugService.WriteLine("Request get error");
                await NavigationService.NavigateToAsync<LoginViewModel>();
                Acr.UserDialogs.UserDialogs.Instance
                    .Alert($"{userData.Item1}"); //alert user of the http request message
            }
        }
    }
}
