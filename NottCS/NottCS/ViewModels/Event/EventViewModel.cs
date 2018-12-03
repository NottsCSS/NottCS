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
        public string EventName
        {
            get => _eventName;
            set => SetProperty(ref _eventName, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public string EventImage
        {
            get => _eventImage;
            set => SetProperty(ref _eventImage, value);
        }

        private readonly INavigationService _navigationService;
        private string _eventName;
        private string _description;
        private string _eventImage;
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
