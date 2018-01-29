using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;
using NottCS.Models;

namespace NottCS.ViewModels
{
    class HomeViewModel:BaseViewModel
    {
        public string ClubName { get; set; }
        public string ImageURL { get; set; }
        public ObservableCollection<HomeViewModel> DummyLists { get; set; }
        public static ObservableCollection<HomeViewModel> DummyList { get; set; }
        static HomeViewModel()
        {
            DummyList = new ObservableCollection<HomeViewModel>();
            DummyList.Add(new HomeViewModel
            {
                ClubName = "I'm just a title",
                ImageURL = "http://icons.iconarchive.com/icons/graphicloads/100-flat/24/home-icon.png"
            });
            DummyList.Add(new HomeViewModel
            {

            });
            DummyList.Add(new HomeViewModel
            {

            });
            DummyList.Add(new HomeViewModel
            {

            });
            DummyList.Add(new HomeViewModel
            {

            });

        }
        


        private string _label = "Hello";

        public List<string> ClubTypePickList { get; set; } = new List<string> { "My Clubs Only", "All Clubs", "Favourite Clubs" };

        public string Label
        {
            get => _label;
            set => SetProperty(ref _label, value);
        }
        public string SelectedClubType { get; set; }
        //todo: Navigation when tapped on one of the item and display image of clubs.
        private int Count { get; set; } = 0;
        public ICommand Tapped => new Command(ChangeLabel);
        public ICommand TappedToo => new Command(ChangeLabel);
        private void ChangeLabel()
        {
            Label = $"Hello World {Count}";
            Count++;
            Debug.WriteLine("Button pressed");
        }
        public HomeViewModel()
        {
            SelectedClubType = ClubTypePickList[0];
            DummyLists = DummyList;
        }

    }
}

