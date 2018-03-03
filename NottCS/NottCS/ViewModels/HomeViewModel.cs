using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using NottCS.Models;
using Xamarin.Forms;
using System.Threading.Tasks;
using NottCS.Services.Navigation;
using NottCS.Views;

namespace NottCS.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        #region Temporary EventList
        public ObservableCollection<Item> EventLists { get; set; } = new ObservableCollection<Item>()
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


        #endregion
        #region Temporary ClubList
        private ObservableCollection<Item> _clubList;
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

        #endregion

        public string PageTitle1 { get; set; }
        public string PageTitle2 { get; set; }
        public string PageTitle3 { get; set; }
        
        private string _selectedClubType;
        public bool IsUWP { get; set; }
        public bool IsNotUWP { get; set; }

        #region Picker

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
        private void ChangeLabel1(object e)
        {
            Debug.WriteLine("Picker Changed");
            string picked = e.ToString();
            Debug.WriteLine(picked);
        }

        #endregion
        #region ListViewNavigation

        public ICommand ItemTappedCommand => new Command(async (object p) => await ItemTapped(p));
        private async Task ItemTapped(object p)
        {
            try
            {
                await NavigationService.NavigateToAsync<ClubRegistrationViewModel>(p);
                Debug.WriteLine("Item Tapped");
                
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            
        }

        #endregion
        #region FlowListViewNavigation
        public ICommand TappedCommand => new Command(async (object p) => await Tapped(p));
        private async Task Tapped(object p)
        {
            //Label = $"Hello World {Count}";
            //Count++;
            try
            {
                await NavigationService.NavigateToAsync<EventViewModel>(p);
                Debug.WriteLine("Button pressed");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
        #endregion
        #region HomeViewModel Constructor

        public HomeViewModel()
        {
            _clubList = new ObservableCollection<Item>();
            SelectedClubType = ClubTypePickList[0];
            PageTitle1 = "News Feed";
            PageTitle2 = "Clubs & Society";
            PageTitle3 = "Profile";
            IsUWP = Device.RuntimePlatform == Device.UWP;
            IsNotUWP = Device.RuntimePlatform != Device.UWP;
        }

        #endregion
        //public override Task InitializeAsync(object navigationData)
        //{
        //    if (navigationData is string s)
        //    {
        //        Debug.WriteLine(s);
        //        Debug.WriteLine(navigationData);
        //    }
        //    Debug.WriteLine(navigationData);
        //    return base.InitializeAsync(navigationData);
        //}
        #region Disable ItemSelectedCommand
        public ICommand DisableItemSelectedCommand => new Command(DisableItemSelected);
        public void DisableItemSelected()
        {
        }
        #endregion
    }
}