using System.Collections.Generic;
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;
using NottCS.Services.Navigation;
using System;
using NottCS.Services;

namespace NottCS.ViewModels
{
    internal class SettingsViewModel : BaseViewModel
    {

        public class UserDataObject
        {
            public string Name { get; set; }
            public Type ViewModelType { get; set; }
            public string Logout { get; set; }
        }

        public List<UserDataObject> DummyLists { get; set; }
        public SettingsViewModel()
        {
            Title = "Settings";
            SetPageData();
        }
        public ICommand SettingCommand => new Command(async (object param) => await Setting(param));

        private async Task Setting(object param)
        {
            if (param is UserDataObject tappedUserDataObject)
            {
                try
                {
                    await NavigationService.NavigateToAsync(tappedUserDataObject.ViewModelType);
                }
                catch (Exception e)
                {

                    DebugService.WriteLine(e.Message);
                }
            }
        }
        #region Disable ItemSelectedCommand
        public ICommand DisableItemSelectedCommand => new Command(DisableItemSelected);
        public void DisableItemSelected()
        {
        }
        #endregion
        public ICommand SignOutCommand => new Command(SignOut);

        private static async void SignOut()
        {
            await LoginService.SignOutAndNavigateAsync();
        }

        private void SetPageData()
        {
            DummyLists = new List<UserDataObject>()
            {
                new UserDataObject() {Name = "Notifications", ViewModelType = typeof(NotificationsViewModel)},
                new UserDataObject() {Name = "About", ViewModelType = typeof(AboutViewModel)},
                new UserDataObject() {Name = "Report an Issue", ViewModelType = typeof(ReportIssueViewModel)},
            };
        }
    }
}
