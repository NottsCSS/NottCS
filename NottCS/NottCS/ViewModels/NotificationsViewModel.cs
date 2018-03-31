using System.Diagnostics;
using System.Collections.Generic;
using NottCS.Services.Navigation;
using System;
using System.Windows.Input;
using NottCS.Services;
using Xamarin.Forms;

namespace NottCS.ViewModels
{
    internal class NotificationsViewModel: BaseViewModel
    {
        public class UserDataObject
        {
            public string Name { get; set; }
        }
        public List<UserDataObject> DummyLists { get; set; }

        public NotificationsViewModel()
        {
            //DebugService.WriteLine("Constructor is called");
            Title = "Notifications";
            SetPageData();
        }
        #region Disable ItemSelectedCommand
        public ICommand DisableItemSelectedCommand => new Command(DisableItemSelected);
        public void DisableItemSelected()
        {
        }
        #endregion
        private void SetPageData()
        {
            DummyLists = new List<UserDataObject>()
            {
                new UserDataObject() {Name = "Subscribed Clubs"},
                new UserDataObject() {Name = "Favorite Clubs"},
                new UserDataObject() {Name = "All Clubs"},
            };
        }
    }
}
