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
            try
            {

                AuthenticationResult ar =
                    await App.ClientApplication.AcquireTokenSilentAsync(App.Scopes, App.ClientApplication.Users.FirstOrDefault());
                RefreshUserData(ar.AccessToken);
//                AuthenticationResult ar = await App.ClientApplication.AcquireTokenAsync(App.Scopes, App.UiParent);
//                BaseRestService.SetupClient(ar.AccessToken);
                return ar.AccessToken;
            }
            catch (MsalException ex)
            {
                if (ex.ErrorCode == "user_interaction_required")
                {
                    AuthenticationResult ar = await App.ClientApplication.AcquireTokenAsync(App.Scopes, App.UiParent);
                    return ar.AccessToken;
                }
                else
                {
                    // Here, we catch all other MsalExceptions
                    string message = ex.Message;
                    if (ex.InnerException != null)
                    {
                        message += "Inner Exception : " + ex.InnerException.Message;
                    }
                    return message;
                }
                
            }

        }

        private static async void RefreshUserData(string token)
        {
            //get data from API
            HttpClient client = new HttpClient();
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/v1.0/me");
            message.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
            HttpResponseMessage response = await client.SendAsync(message);
            string responseString = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(response.IsSuccessStatusCode
                ? responseString
                : $"Something went wrong with the API call {responseString}");
        }
    }
}
