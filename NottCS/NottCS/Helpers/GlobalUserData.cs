using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NottCS.Models;
using NottCS.Services;

namespace NottCS.Helpers
{
    internal static class GlobalUserData
    {
        public static User CurrentUser = null;
        public static bool IsValidToken = false;
        public static async void ExpireTokenScheduler()
        {
            var expiryTime = App.MicrosoftAuthenticationResult.ExpiresOn;
            var currentTime = DateTime.Now;
            var timeDiff = (int)expiryTime.Subtract(currentTime).TotalMilliseconds;
            DebugService.WriteLine($"Current time is {currentTime}");
            DebugService.WriteLine($"Token expiring on: {expiryTime}");
            DebugService.WriteLine($"Token expiry to be scheduled in {timeDiff}ms");
            await Task.Delay(timeDiff);
            IsValidToken = false;
            DebugService.WriteLine("Token no longer valid");
        }
    }
}
