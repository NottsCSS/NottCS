using NottCS.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using NottCS.Validations;
using Xamarin.Forms;

namespace NottCS.ViewModels
{
    public class TestViewModel : BaseViewModel
    {
        #region Disable ItemSelectedCommand
        public ICommand DisableItemSelectedCommand => new Command(DisableItemSelected);
        public void DisableItemSelected()
        {
        }
        #endregion
        public ObservableCollection<List<IValidationRule<string>>> ValidationsList { get; } = new ObservableCollection<List<IValidationRule<string>>>()
        {
            new List<IValidationRule<string>>()
            {
                new StringAlphaNumericRule(),
                new StringNotEmptyRule()
            },
            new List<IValidationRule<string>>()
            {
                new StringNumericRule(),
                new StringNotEmptyRule()
            },
            new List<IValidationRule<string>>()
            {
                new StringNotEmptyRule()
            }
        };
        public ObservableCollection<EventAdditionalParameter> ParametersList { get; } = new ObservableCollection<EventAdditionalParameter>()
        {
            new EventAdditionalParameter()
            {
                Name = "Phone manufacturer", ValidationList =
                {
                    new StringNotEmptyRule(),
                    new StringAlphaNumericRule()
                }
            },
            new EventAdditionalParameter()
            {
                Name = "Age", ValidationList =
                {
                    new StringNotEmptyRule(),
                    new StringNumericRule()
                }
            }
        };
        public ICommand SomeCommand => new Command(SomeFunction);
        private void SomeFunction()
        {
            bool isValid = true;
            foreach (var param in ParametersList)
            {
                if (!param.IsValid)
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert(param.ErrorMessage);
                    isValid = false;
                }
            }

            if (isValid)
            {
                //do stuff when all parameters are valid, contact server, navigate, etc.
            }

        }
    }
}
