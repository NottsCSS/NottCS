using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace NottCS.Services
{
    internal class DebugService
    {
        public static void WriteLine(object message)
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                Console.WriteLine(message);
            }
            else
            {
                Debug.WriteLine(message);
            }
        }

        public static void Write(object message)
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                Console.Write(message);
            }
            else
            {
                Debug.Write(message);
            }
        }
    }
}
