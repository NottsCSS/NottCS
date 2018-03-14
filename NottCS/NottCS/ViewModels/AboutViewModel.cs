using System.Diagnostics;
using NottCS.Services;

namespace NottCS.ViewModels
{
    internal class AboutViewModel:BaseViewModel
    {
        public AboutViewModel()
        {
//            DebugService.WriteLine("Constructor is called");
            Title = "About";
        }
    }
}
