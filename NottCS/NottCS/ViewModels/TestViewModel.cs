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
        private string _eventTitle = "some title";

        private EventTime _eventTime1 = new EventTime()
        {
            StartTime = DateTime.Now,
            EndTime = DateTime.Now,
            Id = "5",
            Event = "Some event"
        };
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

        public EventTime EventTime1
        {
            get => _eventTime1;
            set => SetProperty(ref _eventTime1, value);
        }

        public string EventTitle
        {
            get => _eventTitle;
            set => SetProperty(ref _eventTitle, value);
        }

        public ICommand PrintData => new Command(PrintDataFunction);
        public ICommand AddValue => new Command(AddValueFunction);
        public ICommand AddCommand => new Command(AddItem);

        private void AddItem()
        {
            var eventTime = new EventTime();
            EventTimeList.Add(eventTime);
            eventTime.StartTime = DateTime.Now;
            eventTime.EndTime = DateTime.Now;
        }
        private void PrintDataFunction()
        {
            DebugService.WriteLine("----------------");
            //    foreach (var param in EventTimeList)
            //{
            //    DebugService.WriteLine(param.StartTime);
            //    DebugService.WriteLine(param.EndTime);
            //    DebugService.WriteLine(param.Id);
            //    DebugService.WriteLine(param.Event);
            //}
            DebugService.WriteLine(EventTime1.StartTime);
            DebugService.WriteLine(EventTime1.EndTime);

        }

        private void AddValueFunction()
        {
            DebugService.WriteLine("addvalue pressed");
            EventTitle = EventTitle + "2";
            DebugService.WriteLine(EventTitle);
            var event2 = new EventTime()
            {
                StartTime = EventTime1.StartTime + new TimeSpan(1, 1, 1, 1),
                EndTime = EventTime1.EndTime + new TimeSpan(1, 1, 1, 1)
            };
            EventTime1 = event2;

            //foreach (var time in EventTimeList)
            //{
            //    time.StartTime += new TimeSpan(1, 1, 1, 1);
            //    time.EndTime += new TimeSpan(1, 1, 1, 1);
            //}
        }
    }
}
