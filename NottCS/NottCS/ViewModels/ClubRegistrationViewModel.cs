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

namespace NottCS.ViewModels
{
    class ClubRegistrationViewModel : BaseViewModel
    {
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
