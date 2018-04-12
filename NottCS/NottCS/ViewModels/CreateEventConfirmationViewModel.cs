using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using NottCS.Models;
using Xamarin.Forms;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NottCS.Services;
using NottCS.Services.Navigation;
using NottCS.Views;

namespace NottCS.ViewModels
{
    class CreateEventConfirmationViewModel:BaseViewModel
    {
        public ICommand ConfirmationCommand => new Command(DoNothingForNow);
        public void DoNothingForNow()
        {
            //TODO: Pass all item to server through REST Service
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            string ListJson = JsonConvert.SerializeObject(List, settings);
            DebugService.WriteLine(ListJson);
        }

        private ObservableCollection<EventAdditionalParameter> _list;
        public ObservableCollection<EventAdditionalParameter> List
        {
            get => _list;
            set => SetProperty(ref _list, value);
        }

        #region CreateEventConfirmationViewModel Constructor
        public CreateEventConfirmationViewModel()
        {
            Title = "Confirmation";
        }
        #endregion
        #region Disable ItemSelectedCommand
        public ICommand DisableItemSelectedCommand => new Command(DisableItemSelected);
        public void DisableItemSelected()
        {
        }
        #endregion
        public override Task InitializeAsync(object navigationData)
        {
            DebugService.WriteLine(navigationData);

            if (navigationData is ObservableCollection<EventAdditionalParameter> entryList)
            {
                foreach (var item in entryList)
                {
                    DebugService.WriteLine(item.Value);
                }

                List = entryList;
            }
            return base.InitializeAsync(navigationData);
        }
    }
}
