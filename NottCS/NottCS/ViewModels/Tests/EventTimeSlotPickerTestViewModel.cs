using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using NottCS.Models;
using NottCS.Services;
using Xamarin.Forms;

namespace NottCS.ViewModels.Tests
{
    public class EventTimeSlotPickerTestViewModel : BaseViewModel
    {
        public ObservableCollection<EventTime> EventTimeList { get; set; }= new ObservableCollection<EventTime>();

        public ICommand AddCommand => new Command(Add);

        public ICommand DeleteCommand => new Command<EventTime>(Delete);

        private void Delete(EventTime e)
        {
            try
            {
                EventTimeList.Remove(e);
            }
            catch (Exception ex)
            {
                DebugService.WriteLine(ex);
            }
        }
        private void Add()
        {
            EventTimeList.Add(new EventTime()
            {
                StartTime = DateTime.Now.AddHours(-4),
                EndTime = DateTime.Now
            });
        }
    }
}
