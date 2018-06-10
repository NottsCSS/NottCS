using System;
using NottCS.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using NottCS.Services;
using NottCS.Validations;
using Xamarin.Forms;

namespace NottCS.ViewModels
{
    public class TestViewModel : BaseViewModel
    {
//        private ObservableCollection<EventTime> _eventTimeList 

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

        public ObservableCollection<EventTime> EventTimeList { get; set; } = new ObservableCollection<EventTime>()
        {
            new EventTime()
            {
                StartTime = DateTime.Now,
                EndTime = DateTime.Now
            }
        };

        public ICommand SomeCommand => new Command(SomeFunction);
        public ICommand AddCommand => new Command(AddItem);

        private void AddItem()
        {
            var eventTime = new EventTime();
            EventTimeList.Add(eventTime);
            eventTime.StartTime = DateTime.Now;
            eventTime.EndTime = DateTime.Now;
        }
        private void SomeFunction()
        {
            DebugService.WriteLine("----------------");
            foreach (var param in EventTimeList)
            {
                DebugService.WriteLine(param.StartTime);
                DebugService.WriteLine(param.EndTime);
                DebugService.WriteLine(param.Id);
                DebugService.WriteLine(param.Event);
            }

        }
    }
}
