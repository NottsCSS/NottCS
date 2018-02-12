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
    class ClubRegistrationViewModel : BaseViewModel
    {
        public ICommand SignUpCommand => new Command(async () => await SignUp());

        private async Task SignUp()
        {
            try
            {
                await NavigationService.NavigateToAsync<QRCodeViewModel>(new QRCodePage());
                Debug.WriteLine("Button Pressed");
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }

        }
    }
}
