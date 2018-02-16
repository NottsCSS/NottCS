using System.Diagnostics;

namespace NottCS.ViewModels
{
    internal class AboutViewModel:BaseViewModel
    {
        public AboutViewModel()
        {
            Debug.WriteLine("Constructor is called");
            Title = "About";
        }
    }
}
