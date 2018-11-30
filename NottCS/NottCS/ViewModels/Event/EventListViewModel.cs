using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NottCS.Services.Data.Event;
using NottCS.Services.Navigation;
using Xamarin.Forms;

namespace NottCS.ViewModels.Event
{
    public class EventListViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IEventService _eventService;
        private ObservableCollection<Models.Event> _eventsLists = new ObservableCollection<Models.Event>();

        public EventListViewModel(INavigationService navigationService, IEventService eventService)
        {
            _navigationService = navigationService;
            _eventService = eventService;
        }

        //public ObservableCollection<Models.Event> EventList
        //{
        //    get => _eventLists;
        //    set => SetProperty(ref _eventLists, value);
        //}
        public ICommand EventListNavigationCommand => new Command(async (object p) => await EventListNavigation(p));
        private async Task EventListNavigation(object p)
        {
            Debug.WriteLine(p);

            try
            {
                await _navigationService.NavigateToAsync<EventViewModel>(p);
                Debug.WriteLine("Initiated navigation to EventPage");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
        public ObservableCollection<Models.Event> EventsList { get; set; } = new ObservableCollection<Models.Event>()
        {
            new Models.Event()
            {
                Id = 0,
                Title = "Nothing here",
                Description = "Just nothing here",
                EventImage = "https://blog.mozilla.org/firefox/files/2017/12/firefox-logo-600x619.png"
            },
            new Models.Event()
            {
                Id = 1,
                Title = "Something here",
                Description = "Just something here",
                EventImage = "https://blog.mozilla.org/firefox/files/2017/12/firefox-logo-600x619.png"
            },
        };
        

    }
}
