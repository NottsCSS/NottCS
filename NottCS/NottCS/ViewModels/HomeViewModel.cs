using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NottCS.ViewModels
{
    class HomeViewModel:BaseViewModel
    {
        private string _label = "Hello";
        private int Count { get; set; } = 0;
        public string ClubName { get; set; }
        public string ImageURL { get; set; }
        public string SelectedClubType { get; set; }
        public string Label
        {
            get => _label;
            set => SetProperty(ref _label, value);
        }
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
        public List<string> ClubTypePickList { get; set; } = new List<string> { "My Clubs Only", "All Clubs", "Favourite Clubs" };
        
        //todo: Navigation when tapped on one of the item and display image of clubs.
        public ICommand Tapped => new Command(() => ChangeLabel());
        public void ChangeLabel()
        {
            Label = $"Hello World {Count}";
            Count++;
            Debug.WriteLine(Label);
            Debug.WriteLine("Button pressed");
        }
        public HomeViewModel()
        {
            SelectedClubType = ClubTypePickList[0];
            DummyLists = DummyList;
        }

    }
}

