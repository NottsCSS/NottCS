
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using NottCS.Models;
using NottCS.PrivateData;
using NottCS.Services.Navigation;
using Xamarin.Forms;

namespace NottCS.Services.LoginService
{
    public class LoginService : ILoginService
    {
        private readonly ILogger<LoginService> _logger;

        public LoginService(ILogger<LoginService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Attempts to log into Microsoft Graph API
        /// Exception handling required at ViewModel to understand what error and what to display\
        /// Requires set up to connect to backend after authenticating this
        /// </summary>
        /// <returns></returns>
        public async Task<AuthenticationResult> SignInAsync()
        {
            _logger.LogInformation("MSAL Sign in attempted");

            IEnumerable<IAccount> accounts = await App.ClientApplication.GetAccountsAsync();
            AuthenticationResult ar = null;
            try
            {
                IAccount firstAccount = accounts.FirstOrDefault();
                ar = await App.ClientApplication.AcquireTokenSilentAsync(Config.MicrosoftAppScopes, firstAccount);
            }
            catch (MsalUiRequiredException)
            {
                _logger.LogDebug("Ui required for authentication");
                ar = await App.ClientApplication.AcquireTokenAsync(Config.MicrosoftAppScopes, App.UiParent);
            }

            var expectedDomain = "nottingham.edu.my";
            if (!(ar.Account.Username.Contains(expectedDomain)))
            {
                await SignOutAsync();
                throw new MicrosoftAccountException($"Account does not belong to {expectedDomain}");
            }
                

            _logger.LogInformation("MSAL sign in successful");
            _logger.LogDebug($"Auth token: {ar.AccessToken}");
            return ar;
        }

        public async Task SignOutAsync()
        {
            var accounts = await App.ClientApplication.GetAccountsAsync();
            var enumerable = accounts as IAccount[] ?? accounts.ToArray();
            foreach (var account in enumerable)
            {
                await App.ClientApplication.RemoveAsync(account);
            }

            var a = await App.ClientApplication.GetAccountsAsync();
            if (a.Count() != 0)
            {
                throw new Exception("Sign out error, did not completely remove all accounts");
            }
            _logger.LogInformation("Signed out successfully");
        }

        /// <summary>
        /// Handles exceptions and result of AcquireToken methods of MSAL
        /// </summary>
        /// <returns>
        /// CanAuthenticateWithMicrosoft
        /// </returns>
/*        public static async Task<bool> SignInMicrosoftAsync()
        {
            try
            {
                AuthenticationResult ar = await InternalSignInMicrosoft();
                if (ar == null)
                {
                    return false;
                }
                Task setupClientTask = Task.Run(() => RestService.SetupClient(ar.AccessToken));
                App.MicrosoftAuthenticationResult = ar;

                MailAddress email = new MailAddress(ar.User.DisplayableId);
                string host = email.Host;
                if (host != "nottingham.edu.my")
                {
                    DebugService.WriteLine("Not nottingham email");
                    await SignOutAndNavigateAsync();
                    return false;
                }

                DebugService.WriteLine("Login to microsoft successful");
                DebugService.WriteLine($"Welcome {ar.User.Name}");
                DebugService.WriteLine($"Token expires on: {ar.ExpiresOn}");
                DebugService.WriteLine($"Access token: {ar.AccessToken}");
                GlobalUserData.IsValidToken = true;
                GlobalUserData.ExpireTokenScheduler();
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
                bool hasInternet = CrossConnectivity.Current.IsConnected;
                if (hasInternet)
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert($"Http request error: {httpException}");
                }
                else
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert($"No internet connection, try again later");
                }
            }
            catch (Exception e)
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert($"Non-msal exception thrown by MSAL, error: {e}");
            }

            return false;
        }*/

        /// <summary>
        /// Connects to backend and handles data from backend
        /// </summary>
        /// <returns></returns>
        /*public static async Task SignInBackendAndNavigateAsync()
        {
            var userData = await RestService.RequestGetAsync<User>("MustAddUsername");
            if (userData.Item1 == "OK") //first item represents whether the request is successful
            {
                var result = await userData.Item2;
                //if either studentId or librarynumber is not filled that means is new user
                if (String.IsNullOrEmpty(result.StudentId) ||
                    String.IsNullOrEmpty(result.LibraryNumber) ||
                    String.IsNullOrEmpty(result.Course))
                {
                    DebugService.WriteLine("User is not registered");
                    await NavigationService.NavigateToAsync<RegistrationViewModel>(result);
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
        }*/
    }
}