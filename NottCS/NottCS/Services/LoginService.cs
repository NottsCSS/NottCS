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
        public static async Task<string> Authenticate()
        {
            AuthenticationResult ar;
            try
            {
                ar = await App.ClientApplication.AcquireTokenSilentAsync(App.Scopes,
                    App.ClientApplication.Users.FirstOrDefault());
                //                RefreshUserData(ar.AccessToken);
                BaseRestService.SetupClient(ar.AccessToken);

                App.MicrosoftAuthenticationResult = ar;
                BaseRestService.SetupClient(ar.AccessToken);
                return ar.AccessToken;
            }
            catch (MsalException ex)
            {
                if (ex.ErrorCode == "user_interaction_required")
                {
                    Debug.WriteLine(ex.ErrorCode);
                    ar = await InternalAuthenticate();
                    //                    ar = InternalAuthenticate().Result;
                    BaseRestService.SetupClient(ar.AccessToken);
                    return ar.AccessToken;
                }
                else if (ex.ErrorCode == "user_null")
                {
                    Debug.WriteLine(ex.ErrorCode);
                    ar = await InternalAuthenticate();
                    Debug.WriteLine(ar.User.Name);
                    //                    ar = InternalAuthenticate().Result;
                    BaseRestService.SetupClient(ar.AccessToken);
                    return ar.AccessToken;
                }
                else
                {
                    Debug.WriteLine($"Unknown MsalException: {ex.Message}");
                    Debug.WriteLine($"Error code: {ex.ErrorCode}");
                    return ex.Message;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unknown  Exception occured: {ex.Message}");
                ar = await InternalAuthenticate();
                BaseRestService.SetupClient(ar.AccessToken);
                return ar.AccessToken;
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
            AuthenticationResult ar = await App.ClientApplication.AcquireTokenAsync(App.Scopes, App.UiParent);
            BaseRestService.SetupClient(ar.AccessToken);
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
