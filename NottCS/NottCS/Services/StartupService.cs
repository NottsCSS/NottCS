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

    internal static class StartupService
    {
        internal static async Task InitializeAsync()
        {
            //Loading Dialog
            Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Beep beep bop...");

            bool microsoftCanAuthenticate = await LoginService.SignInMicrosoftAsync();
            if (microsoftCanAuthenticate)
                await LoginService.SignInBackendAndNavigateAsync();
            else
            {
                await NavigationService.NavigateToAsync<LoginViewModel>();
            }
            Acr.UserDialogs.UserDialogs.Instance.HideLoading();
        }
    }
}
