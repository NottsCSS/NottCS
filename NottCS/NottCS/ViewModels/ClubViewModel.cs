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
using Newtonsoft.Json;

namespace NottCS.ViewModels
{
    class ClubViewModel : BaseViewModel
    {
        public string PageTitle1 { get; set; }
        public string PageTitle2 { get; set; }

        #region Event List
        #region ListViewNavigation
        public ICommand TappedCommand => new Command(async (object p) => await Tapped(p));
        private async Task Tapped(object p)
        {
            //Label = $"Hello World {Count}";
            //Count++;
            try
            {
                await NavigationService.NavigateToAsync<EventRegistrationViewModel>(p);
                DebugService.WriteLine("Button pressed");
            }
            catch (Exception e)
            {
                DebugService.WriteLine(e.Message);
            }
        }
        #endregion
        #region Disable ItemSelectedCommand
        public ICommand DisableItemSelectedCommand => new Command(DisableItemSelected);
        public void DisableItemSelected()
        {
        }
        #endregion
        #region Temporary EventList
        public ObservableCollection<Item> EventLists { get; set; } = new ObservableCollection<Item>()
        {
            new Item()
            {
                EventName = "I'm just a title",
                ImageURL = "http://icons.iconarchive.com/icons/graphicloads/100-flat/24/home-icon.png"
            },
            new Item(),
            new Item(),
            new Item()
        };


        #endregion
        #endregion
        #region Profile

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

        #endregion
        #region Constructor

        public ClubViewModel()
        {
            PageTitle1 = "Event";
            PageTitle2 = "Club's Profile";
        }

        #endregion

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