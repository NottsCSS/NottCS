using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;
using NottCS.Models;
using NottCS.List;

namespace NottCS.ViewModels
{
    class HomeViewModel:BaseViewModel
    {
        public ObservableCollection<Item> DummyLists { get; set; }
        public HomeViewModel()
        {
            SelectedClubType = ClubTypePickList[0];
            DummyLists = DummyListView.DummyList;
        }


        private string _label = "Hello";

        public List<string> ClubTypePickList { get; set; } = new List<string> { "My Clubs Only", "All Clubs", "Favourite Clubs" };

        public string Label
        {
            get => _label;
            set => SetProperty(ref _label, value);
        }

        private int Count { get; set; } = 0;
        //todo: Navigation when tapped on one of the item and display image of clubs.
        public ICommand Tapped => new Command(ChangeLabel);
        public ICommand TappedToo => new Command(ChangeLabel);

        public string SelectedClubType { get; set; }

        private void ChangeLabel()
        {
            Label = $"Hello World {Count}";
            Count++;
            Debug.WriteLine("Button pressed");
        }

    }
}

