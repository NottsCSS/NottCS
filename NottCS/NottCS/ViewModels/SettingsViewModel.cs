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
            public Type ViewModelType { get; set; }
            public string Logout { get; set; }
        }

        public List<UserDataObject> DummyLists { get; set; }
        public SettingsViewModel()
        {
            Title = "Settings";
            SetPageData();
        }
        public ICommand SettingCommand => new Command(async (object param) => await About(param));

        private async Task About(object param)
        {
            if (param is UserDataObject tappedUserDataObject)
            {
                try
                {
                    await NavigationService.NavigateToAsync(tappedUserDataObject.ViewModelType);
                }
                catch (Exception e)
                {

                    Debug.WriteLine(e.Message);
                }
            }
        }

        private void SetPageData()
        {
            DummyLists = new List<UserDataObject>()
            {
                new UserDataObject() {Name = "Notifications"},
                new UserDataObject() {Name = "About", ViewModelType = typeof(AboutViewModel)},
                new UserDataObject() {Name = "Report an Issue"},
                new UserDataObject() {Logout = "Logout"}
            };
        }
    }
}
