using System;
using NottCS.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using NottCS.Services;
using NottCS.Validations;
using Xamarin.Forms;

namespace NottCS.ViewModels
{
    public class TestViewModel : BaseViewModel
    {
        public bool ShowDeleteBox { get; set; } = false;
        public class TitledEventTime
        {
            public string Title { get; set; } = "Default title";

            public EventTime EventTimeSlot { get; set; } = new EventTime
            {
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
                Event = "-1",
                Id = "-1"
            };
        }


        public ObservableCollection<string> TitleList { get; set; } = new ObservableCollection<string>()
        {
            "1",
            "2"
        };

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

        public ObservableCollection<TitledEventTime> EventTimeList { get; set; } = new ObservableCollection<TitledEventTime>()
        {
            new TitledEventTime()
        };
        
        public ICommand PrintData => new Command(PrintDataFunction);
        public ICommand AddCommand => new Command(AddItem);
        public ICommand DeleteRowCommand => new Command(DeleteItem);

        private void DeleteItem(object item)
        {
            EventTimeList.Remove((TitledEventTime)item);
            DateTimeListChanged();
            ShowDeleteBox = EventTimeList.Count > 1;
        }

        private void AddItem()
        {

//            foreach (var eventTime in EventTimeList)
//            {
//                eventTime.StartTime += new TimeSpan(1, 1, 0, 0);
//                OnPropertyChanged(nameof(eventTime.StartTime));
//            }
            EventTimeList.Add(new TitledEventTime());
            DateTimeListChanged();
            ShowDeleteBox = EventTimeList.Count > 1;
        }
        private void PrintDataFunction()
        {
            DebugService.WriteLine("----------------");
                foreach (var param in EventTimeList)
            {
                DebugService.WriteLine(param.EventTimeSlot.StartTime);
                DebugService.WriteLine(param.EventTimeSlot.EndTime);
                DebugService.WriteLine(param.EventTimeSlot.Id);
                DebugService.WriteLine(param.EventTimeSlot.Event);
            }
        }
        private void DateTimeListChanged()
        {
            DebugService.WriteLine("EventTimeList changed");
            for (int i = 0; i < EventTimeList.Count; i++)
            {
                var oldEventTime = EventTimeList[i];
                var newEventTime = new TitledEventTime()
                {
                    EventTimeSlot = oldEventTime.EventTimeSlot,
                    Title = $"Time Slot {i}"
                };
                EventTimeList[i] = newEventTime;
            }
        }


        
    }
}
