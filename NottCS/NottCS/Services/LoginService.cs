using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using NottCS.Services.REST;

namespace NottCS.Services
{
    public static class LoginService
    {
        public static async Task<string> Authenticate()
        {
            try
            {
                AuthenticationResult ar = await App.ClientApplication.AcquireTokenAsync(App.Scopes, App.UiParent);
                BaseRestService.SetupClient(ar.AccessToken);
                Debug.WriteLine($"Access Token : {ar.AccessToken}");
                return ar.AccessToken;
            }
            catch (MsalException ex)
            {
                return ex.Message;
            }

        }
    }
}
