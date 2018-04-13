using System;
using System.Windows.Input;
using NottCS.Models;
using Xamarin.Forms;
using System.Threading.Tasks;
using NottCS.Services;
using NottCS.Services.Navigation;

namespace NottCS.ViewModels
{
    class EventRegistrationViewModel : BaseViewModel
    {
        public string EventName { get; set; }
        public ICommand SignupCommand => new Command(async () => await Navigate());
        private async Task Navigate()
        {
            try
            {
                await NavigationService.NavigateToAsync<EventRegistrationFormsViewModel>();
                DebugService.WriteLine("Button pressed");
            }
            catch (Exception e)
            {
                DebugService.WriteLine(e.Message);
            }
        }

        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData is Item s)
            {
                DebugService.WriteLine($"Club name is {s.ClubName}");
            }
            return base.InitializeAsync(navigationData);
        }

        public EventRegistrationViewModel()
        {
            EventName = "Some Event Name";
        }
    }
}
