using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using NottCS.Models;
using Xamarin.Forms;
using System.Threading.Tasks;
using NottCS.Services;
using NottCS.Services.Navigation;
using NottCS.Services.REST;

namespace NottCS.ViewModels
{
    class ClubViewModel : BaseViewModel
    {
        public string PageTitle1 { get; set; }
        public string PageTitle2 { get; set; }
        public string ClubDescription { get; set; }

        public ICommand AdminPanelNavigationCommand => new Command(async () => await AdminPanelNavigate());
        private async Task AdminPanelNavigate()
        {
            try
            {
                await NavigationService.NavigateToAsync<AdminPanelViewModel>();
                DebugService.WriteLine("Initiated navigation to AdminPanelPage");
            }
            catch (Exception e)
            {
                DebugService.WriteLine(e.Message);
            }
        }

        #region Event List
        #region ListViewNavigation
        public ICommand EventListNavigationCommand => new Command(async (object p) => await EventListNavigation(p));
        private async Task EventListNavigation(object p)
        {
            //Label = $"Hello World {Count}";
            //Count++;
            try
            {
                await NavigationService.NavigateToAsync<EventRegistrationViewModel>(p);
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
        #region Temporary EventList
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
        private ObservableCollection<Event> _eventLists = new ObservableCollection<Event>();
        public ObservableCollection<Event> EventList
        {
            get => _eventLists;
            set => SetProperty(ref _eventLists, value);
        }


        #endregion

        #region Reload EventList
        private Command _reloadEventCommand;
        public Command ReloadEventCommand
        {
            get
            {
                return _reloadEventCommand ?? (_reloadEventCommand = new Command(ExecuteReloadEventCommand, () => !IsBusy));
            }
        }
        private async void ExecuteReloadEventCommand()
        {
            if (IsBusy)
                return;
            DebugService.WriteLine("Reload Event Initiated");
            IsBusy = true;
            ReloadEventCommand.ChangeCanExecute();

            await LoadEventList();

            IsBusy = false;
            ReloadEventCommand.ChangeCanExecute();
            DebugService.WriteLine("Event Reload Completed");
            
        }
        #endregion
        #endregion
        #region Profile

        public ICommand SignUpCommand => new Command(async () => await SignUp());

        private async Task SignUp()
        {
            try
            {
                await NavigationService.NavigateToAsync<QRCodeViewModel>();
                DebugService.WriteLine("Button Pressed");
            }
            catch (Exception e)
            {
                DebugService.WriteLine(e.Message);
            }
        }

        #endregion
        #region ClubViewModel Constructor

        public ClubViewModel()
        {
            PageTitle1 = "Event";
            PageTitle2 = "Club's Profile";
            ClubDescription = "Lorem ipsum dolor sit amet, consectetur  adipiscing elit, sed do eiusmod tempor incididunt  ut labore et dolore magna aliqua. Ut enim ad  minim veniam, quis nostrud exercitation ullamco  laboris nisi ut aliquip ex ea commodo consequat.";
            LoadEventList().GetAwaiter();
        }

        #endregion

        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData is Item s)
            {
                DebugService.WriteLine($"Club name is {s.ClubName}");
            }

            return base.InitializeAsync(navigationData);
        }
    }
}