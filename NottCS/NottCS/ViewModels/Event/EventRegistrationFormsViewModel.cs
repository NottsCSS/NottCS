using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NottCS.Models;
using NottCS.Services.Data.User.NottCS.Services.Data.Club;
using NottCS.Services.Navigation;
using Xamarin.Forms;

namespace NottCS.ViewModels.Event
{
    public class EventRegistrationFormsViewModel:BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IUserService _userService;
        private User _user;

        public EventRegistrationFormsViewModel(INavigationService navigationService, IUserService userService)
        {
            _navigationService = navigationService;
            _userService = userService;
            var i = InitializeAsync(); //just to suppress warning
        }

        public ObservableCollection<Forms> TemporaryForms { get; set; } = new ObservableCollection<Forms>();
       

        #region EventRegistrationNavigation
        public ICommand SignUpCommand => new Command(async () => await SignUpAndNavigate());

        private async Task SignUpAndNavigate()
        {
            IsBusy = true;
            try
            {
                foreach (var item in TemporaryForms)
                {
                    Debug.WriteLine(item.Fields);
                }

                Debug.WriteLine("Navigation initiated");
                await _navigationService.NavigateToAsync<EventRegistrationSuccessViewModel>();
                
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion

        private async Task InitializeAsync()
        {
            _user = await _userService.GetUser();
        }

    }
}
