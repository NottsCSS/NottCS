using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace NottCS.Services
{
    public static class LoginService
    {
        public static async Task<string> Authenticate()
        {
            try
            {
                AuthenticationResult ar = await App.ClientApplication.AcquireTokenAsync(App.Scopes, App.UiParent);
                return $"Welcome {ar.User.Name}";
            }
            catch (MsalException ex)
            {
                return ex.Message;
            }

        }
    }
}
