using System;
using System.Collections.Generic;
using System.Text;
using NottCS.Models;

namespace NottCS.Helpers
{
    internal static class GlobalUserData
    {
        public static User CurrentUser { get; set; } = null;
    }
}
