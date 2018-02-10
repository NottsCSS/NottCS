using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using NottCS.Models;
using Xamarin.Forms;

namespace NottCS.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public ObservableCollection<Item> DummyLists { get; set; } = new ObservableCollection<Item>()
        {
            new Item()
            {
                ClubName = "I'm just a title",
                ImageURL = "http://icons.iconarchive.com/icons/graphicloads/100-flat/24/home-icon.png"
            },
            new Item(),
            new Item(),
            new Item()
        };
        public ObservableCollection<Item> ClubList
        {
            get => _clubList; 
            set => SetProperty(ref _clubList, value); 
        }

        public ObservableCollection<Item> FavouriteClubList { get; set; } = new ObservableCollection<Item>()
        {
            new Item()
            {
                ClubName = "ClubName",
                ImageURL = "http://icons.iconarchive.com/icons/graphicloads/100-flat/24/home-icon.png"
            },
            new Item()
            {
                ClubName = "ClubName1",
                ImageURL = "http://icons.iconarchive.com/icons/graphicloads/100-flat/24/home-icon.png"
            },
            new Item()
            {
                ClubName = "ClubName2",
                ImageURL = "http://icons.iconarchive.com/icons/graphicloads/100-flat/24/home-icon.png"
            },
            new Item()
            {
                ClubName = "ClubName3",
                ImageURL = "http://icons.iconarchive.com/icons/graphicloads/100-flat/24/home-icon.png"
            },
            new Item()
            {
                ClubName = "ClubName4",
                ImageURL = "http://icons.iconarchive.com/icons/graphicloads/100-flat/24/home-icon.png"
            },
            new Item()
            {
                ClubName = "ClubName5",
                ImageURL = "http://icons.iconarchive.com/icons/graphicloads/100-flat/24/home-icon.png"
            },
            new Item()
            {
                ClubName = "ClubName6",
                ImageURL = "http://icons.iconarchive.com/icons/graphicloads/100-flat/24/home-icon.png"
            },
            new Item()
            {
                ClubName = "ClubName7",
                ImageURL = "http://icons.iconarchive.com/icons/graphicloads/100-flat/24/home-icon.png"
            },
        };
        public ObservableCollection<Item> AllClubList { get; set; } = new ObservableCollection<Item>()
        {
            new Item()
            {
                ClubName = "ClubName",
                ImageURL = "http://icons.iconarchive.com/icons/graphicloads/100-flat/24/home-icon.png"
            },
            new Item()
            {
                ClubName = "ClubName1",
                ImageURL = "http://icons.iconarchive.com/icons/graphicloads/100-flat/24/home-icon.png"
            },
            new Item()
            {
                ClubName = "ClubName2",
                ImageURL = "http://icons.iconarchive.com/icons/graphicloads/100-flat/24/home-icon.png"
            },
            new Item()
            {
                ClubName = "ClubName3",
                ImageURL = "http://icons.iconarchive.com/icons/graphicloads/100-flat/24/home-icon.png"
            },
            new Item()
            {
                ClubName = "ClubName4",
                ImageURL = "http://icons.iconarchive.com/icons/graphicloads/100-flat/24/home-icon.png"
            },
            new Item()
            {
                ClubName = "ClubName5",
                ImageURL = "http://icons.iconarchive.com/icons/graphicloads/100-flat/24/home-icon.png"
            },
            new Item()
            {
                ClubName = "ClubName6",
                ImageURL = "http://icons.iconarchive.com/icons/graphicloads/100-flat/24/home-icon.png"
            }
        };
        public ObservableCollection<Item> MyClubList { get; set; } = new ObservableCollection<Item>()
        {
            new Item()
            {
                ClubName = "ClubName",
                ImageURL = "http://icons.iconarchive.com/icons/graphicloads/100-flat/24/home-icon.png"
            },
            new Item()
            {
                ClubName = "ClubName1",
                ImageURL = "http://icons.iconarchive.com/icons/graphicloads/100-flat/24/home-icon.png"
            },
            new Item()
            {
                ClubName = "ClubName2",
                ImageURL = "http://icons.iconarchive.com/icons/graphicloads/100-flat/24/home-icon.png"
            },
            new Item()
            {
                ClubName = "ClubName3",
                ImageURL = "http://icons.iconarchive.com/icons/graphicloads/100-flat/24/home-icon.png"
            },
            new Item()
            {
                ClubName = "ClubName4",
                ImageURL = "http://icons.iconarchive.com/icons/graphicloads/100-flat/24/home-icon.png"
            }
        };

        public string PageTitle1 { get; set; }
        public string PageTitle2 { get; set; }
        public string PageTitle3 { get; set; }
        private ObservableCollection<Item> _clubList;
        private string _label = "Hello";
        private string _label1 = "Hello";
        private string _selectedClubType;
        private int Count { get; set; } = 0;
        private int Count1 { get; set; } = 0;
        public string Label
        {
            get => _label;
            set => SetProperty(ref _label, value);
        }

        public string Label1
        {
            get => _label1;
            set => SetProperty(ref _label1, value);
        }

        public List<string> ClubTypePickList { get; set; } =
            new List<string> {"My Clubs Only", "All Clubs", "Favourite Clubs"};

        public string SelectedClubType
        {
            get => _selectedClubType;
            set
            {
                ChangeLabel1(value);
                _selectedClubType = value;
                switch (value)
                {
                    case "My Clubs Only":
                        ClubList = MyClubList;
                        break;
                    case "All Clubs":
                        ClubList = AllClubList;
                        break;
                    case "Favourite Clubs":
                        ClubList = FavouriteClubList;
                        break;
                }
            }
        }
        //todo: Navigation when tapped on one of the item and display image of clubs.
        public ICommand ItemTappedCommand => new Command(ItemTapped);
        public ICommand Tapped => new Command(ChangeLabel);
        private void ItemTapped()
        {
            Debug.WriteLine("Item Tapped");
        }

        private void ChangeLabel()
        {
            Label = $"Hello World {Count}";
            Count++;
            Debug.WriteLine("Button pressed");
        }

        private void ChangeLabel1(object e)
        {
            Label1 = $"Hello World {Count1}";
            Count1++;
            Debug.WriteLine("Picker Changed");
            string picked = e.ToString();
            Debug.WriteLine(picked);
        }
        

        public HomeViewModel()
        {
            _clubList = new ObservableCollection<Item>();
            SelectedClubType = ClubTypePickList[0];
            PageTitle1 = "News Feed";
            PageTitle2 = "Clubs & Society";
            PageTitle3 = "Profile";
        }
    }
}