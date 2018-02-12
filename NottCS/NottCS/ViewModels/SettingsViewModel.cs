using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;
using NottCS.Views;
using NottCS.Services.Navigation;
using System;

namespace NottCS.ViewModels
{
    internal class SettingsViewModel : BaseViewModel
    {

        public class UserDataObject
        {
            public string Name { get; set; }
            public string Logout { get; set; }
        }

        public List<UserDataObject> DummyLists { get; set; }
        public SettingsViewModel()
        {
            Debug.WriteLine("Constructor is called");
            Title = "Settings";
            SetPageData();
        }
        public ICommand SettingCommand => new Command(async () => await About());

        private async Task About()
        {
            try {
                await NavigationService.NavigateToAsync<AboutViewModel>(new AboutPage());
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }
        }

        private void SetPageData()
        {
            DummyLists = new List<UserDataObject>()
            {
                new UserDataObject() {Name = "Notifications"},
                new UserDataObject() {Name = "About Us"},
                new UserDataObject() {Name = "Report an Issue"},
                new UserDataObject() {Logout = "Logout"}
            };
        }
    }
}
