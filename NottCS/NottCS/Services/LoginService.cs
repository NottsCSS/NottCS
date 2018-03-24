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
    //TODO: problem: calling login does not lead into registration, only can get into registration on app startup
    //TODO: check what happens when token expires, and call AcquireTokenSilentAsync with no internet connection
    public static class LoginService
    {
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

        public static async Task<bool> SignInMicrosoftAsync()
        {
            try
            {
                AuthenticationResult ar = await InternalSignInMicrosoft();
                App.MicrosoftAuthenticationResult = ar;

                RestService.SetupClient(ar.AccessToken);
                DebugService.WriteLine("Login to microsoft successful");
                DebugService.WriteLine($"Welcome {ar.User.Name}");
                DebugService.WriteLine($"Token expires on: {ar.ExpiresOn}");
                DebugService.WriteLine($"Access token: {ar.AccessToken}");

                return true;
            }
            catch (MsalServiceException serviceException)
            {
                switch (serviceException.ErrorCode)
                {
                    case MsalServiceException.RequestTimeout:
                        Acr.UserDialogs.UserDialogs.Instance.Alert("Request timeout",
                            "Microsoft Authentication Service Error", "OK");
                        break;
                    case MsalServiceException.ServiceNotAvailable:
                        Acr.UserDialogs.UserDialogs.Instance.Alert("Service unavailable",
                            "Microsoft Authentication Service Error", "OK");
                        break;
                    default:
                        Acr.UserDialogs.UserDialogs.Instance.Alert("Unknown error occured",
                            "Microsoft Authentication Service Error", "OK");
                        break;
                }
            }
            catch (MsalClientException clientException)
            {
                switch (clientException.ErrorCode)
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
                    default:
                        Acr.UserDialogs.UserDialogs.Instance.Alert("Unknown error occured",
                            "Microsoft Authentication Client Error", "OK");
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

        public static async Task SignInBackendAndNavigateAsync()
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
            }
            else
            {
                await NavigationService.NavigateToAsync<LoginViewModel>();
                Acr.UserDialogs.UserDialogs.Instance
                    .Alert($"{userData.Item1}"); //alert user of the http request message
            }
        }
    }
}
