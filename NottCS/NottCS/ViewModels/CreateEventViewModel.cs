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

        private bool _lessThan3ViewCell;
        private ObservableCollection<EventAdditionalParameter> _listOfTextBox;
        public bool LessThan3ViewCell
        {
            get => _lessThan3ViewCell;
            set => SetProperty(ref _lessThan3ViewCell, value);
        }
        public ObservableCollection<EventAdditionalParameter> ListOfTextBox
        {
            get => _listOfTextBox;
            set => SetProperty(ref _listOfTextBox, value);
        }

        public ICommand CreateTextBoxCommand => new Command(CreateTextBox);
        public ICommand DeleteCellsCommand =>new Command(DeleteTextBox);
        public ICommand CreateEventCommand => new Command(async () => await Navigate());

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
                DebugService.WriteLine("Delete textbox command activated");
            }

            if (ListOfTextBox.Count == 3)
            {
                LessThan3ViewCell = false;
            }
            
        }
        private async Task Navigate()
        {
            try
            {
                foreach (var item in ListOfTextBox)
                {
                    DebugService.WriteLine(item.Value);
                }
                await NavigationService.NavigateToAsync<CreateEventConfirmationViewModel>(ListOfTextBox);
                DebugService.WriteLine("Create Event Button pressed");
            }
            catch (Exception e)
            {
                DebugService.WriteLine(e.Message);
            }
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
