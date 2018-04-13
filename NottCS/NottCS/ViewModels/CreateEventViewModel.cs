using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using NottCS.Models;
using Xamarin.Forms;
using System.Threading.Tasks;
using NottCS.Services;
using NottCS.Services.Navigation;

namespace NottCS.ViewModels
{
    class CreateEventViewModel : BaseViewModel
    {
        //todo Add validation and optional option to admin (Alphabet, Alphanumeric, Numeric and All)
        public bool LessThan3ViewCell
        {
            get => _lessThan3ViewCell;
            set => SetProperty(ref _lessThan3ViewCell, value);
        }
        
        public ICommand CreateTextBoxCommand => new Command(CreateTextBox);
        public ICommand DeleteCellsCommand =>new Command(DeleteTextBox);
        
        private void CreateTextBox()
        {
            ListOfTextBox.Add(new EventAdditionalParameter());
            DebugService.WriteLine("New text box added");
            if (ListOfTextBox.Count > 3)
            {
                LessThan3ViewCell = true;
            }
        }

        private void DeleteTextBox(object p)
        {
            if (ListOfTextBox.Count > 3)
            {
                DebugService.WriteLine(p);
                ListOfTextBox.Remove((EventAdditionalParameter)p);
                DebugService.WriteLine(ListOfTextBox.Count);
                DebugService.WriteLine("Delete command activated");
            }

            if (ListOfTextBox.Count == 3)
            {
                LessThan3ViewCell = false;
            }
            
        }

        public void DoNothingForNow()
        {
            
            foreach (var item in ListOfTextBox)
            {
                DebugService.WriteLine(item.Value);
            }
        }

        #region CreateEventNavigation
        public ICommand CreateEventCommand => new Command(async () => await Navigate());
        private async Task Navigate()
        {
            try
            {
                DoNothingForNow();
                await NavigationService.NavigateToAsync<CreateEventConfirmationViewModel>(ListOfTextBox);
                DebugService.WriteLine("Button pressed");
            }
            catch (Exception e)
            {
                DebugService.WriteLine(e.Message);
            }
        }
        #endregion

        private ObservableCollection<EventAdditionalParameter> _listOfTextBox;
        private bool _lessThan3ViewCell;

        public ObservableCollection<EventAdditionalParameter> ListOfTextBox
        {
            get => _listOfTextBox;
            set => SetProperty(ref _listOfTextBox, value);
        }
        
        #region Disable ItemSelectedCommand
        public ICommand DisableItemSelectedCommand => new Command(DisableItemSelected);
        public void DisableItemSelected()
        {
        }
        #endregion
        #region Disable ItemSelectedCommand1
        public ICommand DisableItemSelectedCommand1 => new Command(DisableItemSelected1);
        public void DisableItemSelected1()
        {
        }
        #endregion
        #region CreateEventViewModel Constructor
        public CreateEventViewModel()
        {
            ListOfTextBox =new ObservableCollection<EventAdditionalParameter>()
            {
                new EventAdditionalParameter()
                {
                    Name = "Additional Info 1"
                },
                new EventAdditionalParameter()
                {
                    Name = "Additional Info 2"
                },
                new EventAdditionalParameter()
                {
                    Name = "Additional Info 3"
                }
            };
            LessThan3ViewCell = false;
            Title = "Create New Event Instance";


        }

        #endregion
    }
}
