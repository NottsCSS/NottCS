using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using NottCS.Models;
using Xamarin.Forms;
using System.Threading.Tasks;
using NottCS.Services.Navigation;
using NottCS.Views;

namespace NottCS.ViewModels
{
    class CreateEventViewModel : BaseViewModel
    {
        public ICommand CreateTextBoxCommand => new Command(CreateTextBox);
        public ICommand CreateEventCommand => new Command(DoNothingForNow);

        public void CreateTextBox()
        {

        }

        public void DoNothingForNow()
        {

        }

        public ObservableCollection<Item> ListOfTextBox { get; set; } = new ObservableCollection<Item>()
        {
            new Item(),
            new Item(),
            new Item()
        };
        #region Disable ItemSelectedCommand
        public ICommand DisableItemSelectedCommand => new Command(DisableItemSelected);
        public void DisableItemSelected()
        {
        }
        #endregion

        #region CreateEventViewModel Constructor

        public CreateEventViewModel()
        {
        }

        #endregion
    }
}
