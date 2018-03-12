using NottCS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using NottCS.Validations;
using Xamarin.Forms;

namespace NottCS.ViewModels
{
    public class TestViewModel : BaseViewModel
    {

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
        public ICommand SomeCommand => new Command(SomeFunction);
        private void SomeFunction()
        {
            Debug.WriteLine(typeof(IValidationRule<string>));
            var rule1 = new StringNotEmptyRule();
            Type rule1Type = rule1.GetType();
            Debug.WriteLine(rule1Type);
            IValidationRule<string> rule2 = null;
            try
            {
                rule2 = Activator.CreateInstance(rule1Type) as IValidationRule<string>;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.TargetSite);
            }

            Debug.WriteLine($"Rule 2 is: {rule2?.GetType()}");


        }
    }
}
