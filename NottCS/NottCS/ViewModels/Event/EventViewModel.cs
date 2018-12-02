using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NottCS.Services.Navigation;
using Xamarin.Forms;

namespace NottCS.ViewModels.Event
{
    public class EventViewModel:BaseViewModel
    {
        public string EventName { get; set; }
        public string Description { get; set; }
        public string EventImage { get; set; }
        private readonly INavigationService _navigationService;
        public ICommand SignupCommand => new Command(async () => await Navigate());
        private async Task Navigate()
        {
            try
            {
                await _navigationService.NavigateToAsync<EventRegistrationFormsViewModel>();
                Debug.WriteLine("Button pressed");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData is Models.Event s)
            {
                Debug.WriteLine($"Event name is {s.Title}");
                Debug.WriteLine($"Event description is {s.Description}");
                Debug.WriteLine($"Event image is {s.EventImage}");
                EventName = s.Title;
                Description = s.Description;
                EventImage = s.EventImage;
                Debug.WriteLine($"Event name is {EventName}");
                Debug.WriteLine($"Event description is {Description}");
                Debug.WriteLine($"Event image is {EventImage}");
            }
            return base.InitializeAsync(navigationData);
        }

        public EventViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
