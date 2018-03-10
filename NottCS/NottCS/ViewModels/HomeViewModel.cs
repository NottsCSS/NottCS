using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using NottCS.Models;
using Xamarin.Forms;
using System.Threading.Tasks;
using NottCS.Services;
using NottCS.Services.Navigation;
using NottCS.Views;
using Newtonsoft.Json;

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
            DebugService.WriteLine("Picker Changed");
            string picked = e.ToString();
            DebugService.WriteLine(picked);
        }

        #endregion
        #region ListViewNavigation

        public ICommand ItemTappedCommand => new Command(async (object p) => await ItemTapped(p));
        private async Task ItemTapped(object p)
        {
            try
            {
                await NavigationService.NavigateToAsync<ClubRegistrationViewModel>(p);
                DebugService.WriteLine("Item Tapped");
                
            }
            catch (Exception e)
            {
                DebugService.WriteLine(e.Message);
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
                DebugService.WriteLine("Button pressed");
            }
            catch (Exception e)
            {
                DebugService.WriteLine(e.Message);
            }
        }
        #endregion
        #region Disable ItemSelectedCommand
        public ICommand DisableItemSelectedCommand => new Command(DisableItemSelected);
        public void DisableItemSelected()
        {
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

        #region Profile
        #region ViewModalAdditionalClass

        public class UserDataObject
        {
            public string DataName { get; set; }
            public string DataValue { get; set; }
        }

        #endregion

        #region PageProperties
        private User _loginUser;
        private List<UserDataObject> _dataList;

        public User LoginUser
        {
            get => _loginUser;
            set => SetProperty(ref _loginUser, value);
        }

        public List<UserDataObject> DataList
        {
            get => _dataList;
            set => SetProperty(ref _dataList, value);
        }

        #endregion

        public string AccessToken { get; } = App.MicrosoftAuthenticationResult.AccessToken;

        /// <summary>
        /// Sets the data for the page
        /// </summary>
        /// <param name="userData">Username for the account data</param>
        private void SetPageDataAsync(User userData)
        {
            LoginUser = userData;
            DebugService.WriteLine(JsonConvert.SerializeObject(LoginUser));
            DataList = new List<UserDataObject>()
            {
                new UserDataObject(){DataName = "Name", DataValue = LoginUser.Name},
                new UserDataObject(){DataName = "Email", DataValue = LoginUser.Email},
                new UserDataObject(){DataName = "Student ID", DataValue = LoginUser.StudentId},
                new UserDataObject(){DataName = "Library Number", DataValue = LoginUser.LibraryNumber},
                new UserDataObject(){DataName = "Date Joined", DataValue = LoginUser.DateJoined.ToLongDateString()}
            };
        }

        public ICommand SignOutCommand => new Command(SignOut);

        private static void SignOut()
        {
            LoginService.SignOut();
        }

        #endregion
        /// <summary>
        /// Initializes the page
        /// </summary>
        /// <param name="navigationData">Data passed from the previous page</param>
        /// <returns></returns>
        public override Task InitializeAsync(object navigationData)
        {

            try
            {
                var userData = navigationData as User;
                Task.Run(() => SetPageDataAsync(userData));
            }
            catch (Exception e)
            {
                DebugService.WriteLine(e);
            }

            return base.InitializeAsync(navigationData);

            //DebugService.WriteLine("Initializing Account Page...");
            //if (navigationData is string username)
            //{
            //    DebugService.WriteLine("Stage 2...");
            //    var isSuccess = SetPageDataAsync(username).GetAwaiter().GetResult();
            //    if (isSuccess)
            //    {
            //        DebugService.WriteLine("Stage 3...");
            //        return base.InitializeAsync(navigationData);
            //    }
            //}
        }
    }
}