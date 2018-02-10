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
    internal class AboutViewModel:BaseViewModel
    {
        public class UserDataObject
        {
            public string Name { get; set; }
        }

        public AboutViewModel()
        {
            Debug.WriteLine("Constructor is called");
            Title = "About";
            SetPageData();
        }
        public List<UserDataObject> DummyLists { get; set; }

        public ICommand Tapped => new Command(async () => await WhyNottCSExists());

        private async Task WhyNottCSExists()
        {
            try
            {
                await NavigationService.NavigateToAsync<WhyNottCSExistsViewModel>(new WhyNottCSExistsPage());
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
                new UserDataObject() {Name = "Why NottCS Exists"}
            };
        }
    }
}
