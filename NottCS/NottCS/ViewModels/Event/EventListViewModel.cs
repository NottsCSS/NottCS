using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace NottCS.ViewModels.Event
{
    public class EventListViewModel : BaseViewModel
    {
        private ObservableCollection<Models.Event> _eventsLists = new ObservableCollection<Models.Event>();
        //public ObservableCollection<Models.Event> EventList
        //{
        //    get => _eventLists;
        //    set => SetProperty(ref _eventLists, value);
        //}
        public ICommand DisableItemSelectedCommand => new Command(() => { });
        public ObservableCollection<Models.Event> EventsList { get; set; } = new ObservableCollection<Models.Event>()
        {
            new Models.Event()
            {
                Id = "0",
                Title = "Nothing here",
                Description = "Just nothing here",
                EventImage = "https://blog.mozilla.org/firefox/files/2017/12/firefox-logo-600x619.png"
            },
            new Models.Event()
            {
                Id = "1",
                Title = "Something here",
                Description = "Just something here",
                EventImage = "https://blog.mozilla.org/firefox/files/2017/12/firefox-logo-600x619.png"
            },
        };
        

    }
}
