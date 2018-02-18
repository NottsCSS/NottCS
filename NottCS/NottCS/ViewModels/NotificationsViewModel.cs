using System.Diagnostics;
using System.Collections.Generic;
using NottCS.Services.Navigation;
using System;

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
            Debug.WriteLine("Constructor is called");
            Title = "Notifications";
            SetPageData();
        }

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
