using System;
using System.Collections.Generic;
using System.Text;

namespace NottCS.PrivateData
{
    internal static class Config
    {
        //This is the application id found in the portal https://apps.dev.microsoft.com after registering the app
        internal static string MicrosoftApplicationId => "81a5b712-2ec4-4d3f-9324-211f60d0a0c9";
        //This should reflect the scopes defined in https://apps.dev.microsoft.com
        internal static string[] MicrosoftAppScopes => new[] {"User.Read"};
        internal static string EndpointAddress => "https://testing-endpoints.herokuapp.com/";
    }
}
