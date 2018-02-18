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
        public static async Task Authenticate()
        {
            AuthenticationResult ar = null;
            try
            {
                ar = await App.ClientApplication.AcquireTokenSilentAsync(App.Scopes,
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
                    ar = await InternalAuthenticate();
                    //                    ar = InternalAuthenticate().Result;
                    BaseRestService.SetupClient(ar?.AccessToken);
                }
                else if (ex.ErrorCode == "user_null")
                {
                    Debug.WriteLine(ex.ErrorCode);
                    ar = await InternalAuthenticate();
                    
                    //                    ar = InternalAuthenticate().Result;
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
                ar = await InternalAuthenticate();
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

        private static async Task<AuthenticationResult> InternalAuthenticate()
        {
            AuthenticationResult ar = null;
            try
            {
                ar = await App.ClientApplication.AcquireTokenAsync(App.Scopes, App.UiParent);
                Debug.WriteLine(ar);
            }
            catch (MsalException ex)
            {
                //TODO: check appropriate error codes and do appropriate stuff

                if (ex.ErrorCode == "access_denied")
                {
                    Debug.WriteLine("Authentication cancelled");
                }
                else if (ex.ErrorCode == "authentication_ui_failed")
                {
                    Debug.WriteLine("Authentication UI closed");
                }
                Debug.WriteLine(ar);
                Debug.WriteLine($"Unknown MsalException occured with error code: {ex.ErrorCode}");
                Debug.WriteLine(ex);
//                Debug.WriteLine(ex.Message);
                Debug.WriteLine($"Site: {ex.TargetSite}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unknown exception {ex}");
                Debug.WriteLine($"{ex.TargetSite}");
            }

            if (ar == null) return null;

//            BaseRestService.SetupClient(ar.AccessToken);
            await Task.Run(()=>BaseRestService.SetupClient(ar.AccessToken));
            Debug.WriteLine($"time limit: {ar.ExpiresOn}");
            App.MicrosoftAuthenticationResult = ar;
            return ar;
        }
        
        private static async void RefreshUserData(string token)
        {
            //get data from API
            HttpClient client = new HttpClient();
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/me");
            message.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
            HttpResponseMessage response = await client.SendAsync(message);
            string responseString = await response.Content.ReadAsStringAsync();
            Debug.WriteLine("Refresh data response: ");
            Debug.WriteLine(response.IsSuccessStatusCode
                ? responseString
                : $"Something went wrong with the API call {responseString}");
        }
    }
}
