using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace NottCS.Services
{
    internal static class LoginService
    {
        public static async Task<string> Authenticate()
        {
            try
            {
                AuthenticationResult ar = await App.ClientApplication.AcquireTokenAsync(App.Scopes);
                return $"Welcome {ar.User.Name}";
            }
            catch (MsalException ex)
            {
                return ex.Message;
            }

        }
    }
}
