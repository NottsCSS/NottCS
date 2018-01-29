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
        public ObservableCollection<ListViewModel> DummyLists { get; set; }
        public HomeViewModel()
        {
            SelectedClubType = ClubTypePickList[0];
            DummyLists = ListViewModel.DummyList;
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

    }
}

