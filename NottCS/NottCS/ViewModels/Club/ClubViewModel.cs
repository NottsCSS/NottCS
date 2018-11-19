using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NottCS.ViewModels.Club
{
    public class ClubViewModel:BaseViewModel
    {
        public string PageTitle1 { get; set; }
        public string PageTitle2 { get; set; }
        public string ClubDescription { get; set; }

        private ObservableCollection<Models.Event> _eventsLists = new ObservableCollection<Models.Event>();

        public ObservableCollection<Models.Event> EventsList
        {
            get => _eventsLists;
            set => SetProperty(ref _eventsLists, value);
        }
        public ClubViewModel()
        {
            PageTitle1 = "Event";
            PageTitle2 = "Club's Profile";
            ClubDescription = "Lorem ipsum dolor sit amet, consectetur  adipiscing elit, sed do eiusmod tempor incididunt  ut labore et dolore magna aliqua. Ut enim ad  minim veniam, quis nostrud exercitation ullamco  laboris nisi ut aliquip ex ea commodo consequat.";
        }
    }
}
