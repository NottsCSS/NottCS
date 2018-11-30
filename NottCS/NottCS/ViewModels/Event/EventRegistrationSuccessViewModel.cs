using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using NottCS.Services.Navigation;
using Xamarin.Forms;

namespace NottCS.ViewModels.Event
{
    public class EventRegistrationSuccessViewModel:BaseViewModel
    {
        private readonly INavigationService _navigationService;

        public EventRegistrationSuccessViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public ICommand OkTapped => new Command(async () =>await _navigationService.SetDetailPageAsync<HomeViewModel>() );
    }
}
