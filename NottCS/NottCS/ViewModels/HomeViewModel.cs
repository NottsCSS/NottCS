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
        public ObservableCollection<Item> DummyLists { get; set; } = new ObservableCollection<Item>()
        {
            new Item(){
            ClubName = "I'm just a title",
            ImageURL = "http://icons.iconarchive.com/icons/graphicloads/100-flat/24/home-icon.png"},
            new Item(),
            new Item(),
            new Item()
        };
        

        private string _label = "Hello";

        public List<string> ClubTypePickList { get; set; } = new List<string> { "My Clubs Only", "All Clubs", "Favourite Clubs" };

        public string Label1
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
            var a = $"Hello World {Count}";
            Label1 = a;
            Count++;
            Debug.WriteLine("Button pressed");
        }
        public HomeViewModel()
        {
            SelectedClubType = ClubTypePickList[0];
        }

    }
}

