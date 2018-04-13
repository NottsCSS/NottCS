using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NottCS.Models;

namespace NottCS.Helpers
{
    internal static class GlobalUserData
    {
        public static User CurrentUser { get; set; } = null;
        public static bool isValidToken = false;
        public static async void ExpireTokenScheduler()
        {
            var expiryTime = App.MicrosoftAuthenticationResult.ExpiresOn;
            await Task.Delay((int)expiryTime.Subtract(DateTime.Now).TotalMilliseconds);
            isValidToken = false;
        }
    }
}
