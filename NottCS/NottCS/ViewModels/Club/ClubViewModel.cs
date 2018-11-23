using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NottCS.Services.Data.Club;
using NottCS.Services.Navigation;
using Xamarin.Forms;

namespace NottCS.ViewModels.Club
{
    public class ClubViewModel:BaseViewModel
    {
        public string PageTitle1 { get; set; }
        public string PageTitle2 { get; set; }
        private string _clubDescription;
        private readonly INavigationService _navigationService;
        public string ClubDescription
        {
            get => _clubDescription;
            set => SetProperty(ref _clubDescription, value);
        }
        public ICommand SignUpCommand => new Command(async () => await SignUp());

        private async Task SignUp()
        {
            Debug.WriteLine("Temporary Empty");
        }


        #region Constructor

        public ClubViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            PageTitle1 = "Event";
            PageTitle2 = "Club's Profile";
            LoadEventList().GetAwaiter();

        }

        #endregion
        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData is Models.Club s)
            {
                Debug.WriteLine($"Club description is {s.Description}");
                ClubDescription = s.Description;
            }
            return base.InitializeAsync(navigationData);
        }

        #region ClubEvents
        
        private ObservableCollection<Models.Event> _eventLists = new ObservableCollection<Models.Event>();
        public ObservableCollection<Models.Event> EventList
        {
            get => _eventLists;
            set => SetProperty(ref _eventLists, value);
        }

        #region Load/Reload Event

        private async Task LoadEventList()
        {
            Debug.WriteLine("Temporary nth");
        }
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
            Debug.WriteLine("Reload Event Initiated");
            IsBusy = true;
            ReloadEventCommand.ChangeCanExecute();

            await LoadEventList();

            IsBusy = false;
            ReloadEventCommand.ChangeCanExecute();
            Debug.WriteLine("Event Reload Completed");

        }

        #endregion

        #region Event Navigation


        public ICommand EventListNavigationCommand => new Command(async (object p) => await EventListNavigation(p));
        private async Task EventListNavigation(object p)
        {
            Debug.WriteLine("Temporary Empty");
        }

        #endregion
        #endregion

        #region AdminPanel
        public ICommand AdminPanelNavigationCommand => new Command(async () => await AdminPanelNavigate());
        private async Task AdminPanelNavigate()
        {
            try
            {
                await _navigationService.NavigateToAsync<AdminPanelViewModel>();
                Debug.WriteLine("Initiated navigation to AdminPanelPage");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }


        #endregion

    }
}
