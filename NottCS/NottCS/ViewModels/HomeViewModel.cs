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
using NottCS.Helpers;
using NottCS.Services.REST;

namespace NottCS.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public string PageTitle1 { get; set; } = "News Feed";
        public string PageTitle2 { get; set; } = "Clubs & Society";
        public string PageTitle3 { get; set; } = "Profile";

        public HomeViewModel()
        {
            Title = "NottCS";
            SelectedClubTypeIndex = 1;
        }
        /// <summary>
        /// Initializes the page
        /// </summary>
        /// <param name="navigationData">Data passed from the previous page</param>
        /// <returns></returns>
        public override async Task InitializeAsync(object navigationData)
        {
            await LoadClubList();
            await LoadEventList();
            try
            {
                var userData = navigationData as User;
                await Task.Run(() => SetProfileData(userData));
            }
            catch (Exception e)
            {
                DebugService.WriteLine(e);
            }
            SelectedClubTypeIndex = 1;
        }

        public ICommand SettingsPageNavigationCommand =>
            new Command(async () => await NavigationService.NavigateToAsync<SettingsViewModel>());
        public ICommand MediaTestPageNavigationCommand =>
            new Command(async () => await NavigationService.NavigateToAsync<MediaTestViewModel>());

        #region Event Tab
        #region Event Commands and Functions
        //Commands
        public ICommand EventListNavigationCommand => new Command(async (object p) => await EventListNavigation(p));
        public ICommand ReloadEventCommand => new Command(async() => await ReloadEvent());
        public ICommand DisableItemSelectedCommand => new Command(() => {});
        //Functions
        private async Task EventListNavigation(object p)
        {
            try
            {
                await NavigationService.NavigateToAsync<EventRegistrationViewModel>(p);
                DebugService.WriteLine("Initiated navigation to EventRegistrationPage");
            }
            catch (Exception e)
            {
                DebugService.WriteLine(e.Message);
            }
        }
        private async Task ReloadEvent()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                DebugService.WriteLine("Reload Event Initiated");
                await LoadEventList();
                DebugService.WriteLine("Event Reload Completed");
                IsBusy = false;
            }
        }
        private async Task LoadEventList()
        {
            var result = await RestService.RequestGetAsync<Event>();
            DebugService.WriteLine(result);
            var eventList = result.Item2;
            if (result.Item1 != "OK")
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert(result.Item1, "Error", "OK");
            }

            EventList = new ObservableCollection<Event>(eventList);
        }
        #endregion
        //Data
        private ObservableCollection<Event> _eventLists = new ObservableCollection<Event>();
        public ObservableCollection<Event> EventList
        {
            get => _eventLists;
            set => SetProperty(ref _eventLists, value);
        }
        #endregion

        #region Club Tab
        //Picker
        public List<string> ClubListTypePickerList { get; set; } =
            new List<string> { "My Clubs Only", "All Clubs"};
        private int _selectedClubTypeIndex;
        public int SelectedClubTypeIndex
        {
            get => _selectedClubTypeIndex;
            set
            {
                _selectedClubTypeIndex = value;
                switch (value)
                {
                    case 0:
                        ClubList = MyClubList;
                        DebugService.WriteLine("ClubList changed to MyClubList");
                        break;
                    case 1:
                        ClubList = AllClubList;
                        DebugService.WriteLine("ClubList changed to AllClubList");
                        break;
                }
            }
        }

        #region Club Commands and Functions
        public ICommand ClubListNavigationCommand => new Command(async (object p) => await ClubListNavigation(p));
        public ICommand ReloadClubCommand => new Command(async () => await ReloadClub());

        private async Task ClubListNavigation(object p)
        {
            try
            {
                await NavigationService.NavigateToAsync<ClubViewModel>(p);
                DebugService.WriteLine("Initiated navigation to ClubPage");

            }
            catch (Exception e)
            {
                DebugService.WriteLine(e.Message);
            }

        }
        private async Task ReloadClub()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                DebugService.WriteLine("Club Reload Initiated");
                await LoadClubList();
                DebugService.WriteLine("Club Reload Completed");
                IsBusy = false;
            }
        }
        private async Task LoadClubList()
        {
            var result = await RestService.RequestGetAsync<Club>();
            var clubList = result.Item2;
            if (result.Item1 != "OK")
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert(result.Item1, "Error", "OK");
            }

            AllClubList = new ObservableCollection<Club>(clubList);
        }
        #endregion
        #region Club Data Store
        private ObservableCollection<Club> _clubList = new ObservableCollection<Club>();
        public ObservableCollection<Club> ClubList
        {
            get => _clubList;
            set => SetProperty(ref _clubList, value);
        }

        private ObservableCollection<Club> AllClubList { get; set; } = new ObservableCollection<Club>()
        {
        };
        private ObservableCollection<Club> MyClubList { get; set; } = new ObservableCollection<Club>()
        {
        };
        #endregion
        #endregion

        #region Profile tab
        #region Profile Commands and Functions

        /// <summary>
        /// Sets the data for the page
        /// </summary>
        /// <param name="userData">Username for the account data</param>
        private void SetProfileData(User userData)
        {
            LoginUser = userData;
            GlobalUserData.CurrentUser = userData;
            DebugService.WriteLine($"HomeViewModel navigationData serialized: {JsonConvert.SerializeObject(LoginUser)}");
            DataList = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Name", LoginUser.Name),
                new KeyValuePair<string, string>("Email", LoginUser.Email),
                new KeyValuePair<string, string>("Student ID", LoginUser.StudentId),
                new KeyValuePair<string, string>("Library Number", LoginUser.LibraryNumber),
                new KeyValuePair<string, string>("Course", LoginUser.Course),
                new KeyValuePair<string, string>("Year of Study", LoginUser.YearOfStudy)
            };
        }
        public ICommand SignOutCommand => new Command(SignOut);
        private static async void SignOut()
        {
            await LoginService.SignOutAndNavigateAsync();
        }
        #endregion
        #region Profile Data Store
        private User _loginUser;
        private User LoginUser
        {
            get => _loginUser;
            set => SetProperty(ref _loginUser, value);
        }

        private List<KeyValuePair<string, string>> _dataList;
        public List<KeyValuePair<string, string>> DataList
        {
            get => _dataList;
            set => SetProperty(ref _dataList, value);
        }
        #endregion
        #endregion

    }
}