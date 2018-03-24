using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using NottCS.Models;
using NottCS.ViewModels;
using NottCS.Services;
using NottCS.Services.Navigation;
using NottCS.Services.REST;

namespace NottCS.Services
{

    //TODO: split up initialize async to allow more modular code such that loginViewModel can call to check registration
    internal static class StartupService
    {
        internal static async Task InitializeAsync()
        {
            //Loading Dialog
            var loadingDialog = Acr.UserDialogs.UserDialogs.Instance.Loading("Beep beep bop...");
            loadingDialog.Show();

            bool microsoftCanAuthenticate = await LoginService.SignInMicrosoftAsync();
            if (microsoftCanAuthenticate)
                await LoginService.SignInBackendAndNavigateAsync();
            else
            {
                await NavigationService.NavigateToAsync<LoginViewModel>();
            }
            loadingDialog.Hide();
        }
    }
}
