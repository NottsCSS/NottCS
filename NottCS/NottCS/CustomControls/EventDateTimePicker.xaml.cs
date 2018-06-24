using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NottCS.Models;
using NottCS.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace NottCS.CustomControls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EventDateTimePicker : ContentView
	{
	    private bool _updatingUI = false;
        //TODO: Use OnPropertyChanged to do 2 way binding
        public static readonly BindableProperty Title1Property =
	        BindableProperty.Create(nameof(Title1), typeof(string), typeof(EventDateTimePicker), defaultBindingMode:BindingMode.TwoWay, propertyChanged:OnTitleChanged, defaultValue: "TITLE??");

	    public static readonly BindableProperty EventTimeSlotProperty =
	        BindableProperty.Create(nameof(EventTimeSlot), typeof(EventTime), typeof(EventDateTimePicker),
	            new EventTime()
	            {
	                StartTime = DateTime.Now,
	                EndTime = DateTime.Now + new TimeSpan(1, 3, 30, 0),
	                Id = "0",
	                Event = "0"
	            }, BindingMode.TwoWay, propertyChanged:OnEventTimeSlotPropertyChanged);


	    public EventTime EventTimeSlot
	    {
	        get
	        {
	            return (EventTime)GetValue(EventTimeSlotProperty);
            } 
	        set
	        {
                SetValue(EventTimeSlotProperty, value);
	            OnPropertyChanged(nameof(StartDate));
	            OnPropertyChanged(nameof(StartTime));
                OnPropertyChanged(nameof(EndDate));
	            OnPropertyChanged(nameof(EndTime));
            }
	    }
        public string Title1
	    {
	        get => (string)GetValue(Title1Property);
	        set => SetValue(Title1Property, value);
	    }

        private static void OnTitleChanged(BindableObject obj, object oldValue, object newValue)
	    {
            DebugService.WriteLine($"Title changed from {oldValue} to {newValue}");
	        DebugService.WriteLine($"Object type: {obj.GetType()}");
        }

        
	    public DateTime StartDate
	    {
	        get
            {

                return EventTimeSlot.StartTime.Date;
            }
	        set
	        {
	            var oldEventTime = EventTimeSlot;
	            var oldStartTime = oldEventTime.StartTime;
	            var newTime = new DateTime(value.Year, value.Month, value.Day, oldStartTime.Hour, oldStartTime.Minute,
	                oldStartTime.Second);
                var newEventTime = new EventTime()
	            {
	                Id = oldEventTime.Id,
	                Event = oldEventTime.Event,
	                StartTime = newTime,
	                EndTime = oldEventTime.EndTime
	            };
                SetValue(EventTimeSlotProperty, newEventTime );
                DebugService.WriteLine("setting event time");
	        }
	    }

	    public TimeSpan StartTime
	    {
	        get => EventTimeSlot.StartTime.TimeOfDay;
	        set
	        {
	            var oldEventTime = EventTimeSlot;
	            var oldStartTime = oldEventTime.StartTime;
	            var newEventTime = new EventTime()
	            {
	                Id = oldEventTime.Id,
	                Event = oldEventTime.Event,
	                StartTime = oldStartTime.Date + value,
	                EndTime = oldEventTime.EndTime
	            };
                SetValue(EventTimeSlotProperty, newEventTime);
                //                var oldTime = EventTimeSlot.StartTime;
                //	            EventTimeSlot.StartTime = oldTime.Date + value;
            }
	    }
	    public DateTime EndDate
	    {
	        get => EventTimeSlot.EndTime.Date;
	        set
	        {
	            var oldEventTime = EventTimeSlot;
	            var oldEndTime = oldEventTime.EndTime;
	            var newTime = new DateTime(value.Year, value.Month, value.Day, oldEndTime.Hour, oldEndTime.Minute,
	                oldEndTime.Second);
                var newEventTime = new EventTime()
                {
                    Id = oldEventTime.Id,
                    Event = oldEventTime.Event,
                    StartTime = oldEventTime.StartTime,
                    EndTime = newTime
                };
                //                SetValue(EventTimeSlotProperty, newEventTime);
                //EventTimeSlot.EndTime = newTime;
                SetValue(EventTimeSlotProperty, newEventTime);
            }
	    }

	    public TimeSpan EndTime
	    {
	        get => EventTimeSlot.EndTime.TimeOfDay;
	        set
	        {
	            var oldEventTime = EventTimeSlot;
	            var oldEndTime = oldEventTime.EndTime;
	            var newEventTime = new EventTime()
	            {
	                Id = oldEventTime.Id,
	                Event = oldEventTime.Event,
	                StartTime = oldEventTime.StartTime,
	                EndTime = oldEndTime.Date + value
	            };
	            SetValue(EventTimeSlotProperty, newEventTime);
            }
	    }

        public ICommand PrintDataCommand => new Command(PrintData);
	    private void PrintData()
	    {
	        Title1 = Title1 + "1";
            DebugService.WriteLine($"Id: {EventTimeSlot.Id}");
            DebugService.WriteLine($"Event: {EventTimeSlot.Event}");
            DebugService.WriteLine($"StartDateTime: {EventTimeSlot.StartTime}");
            DebugService.WriteLine($"EndDateTime: {EventTimeSlot.EndTime}");
	        DebugService.WriteLine($"StartDate: {StartDate}");
	        DebugService.WriteLine($"EndDate: {EndDate}");
	        DebugService.WriteLine($"StartTime: {StartTime}");
	        DebugService.WriteLine($"EndTime: {EndTime}");
        }

        public EventDateTimePicker ()
		{
			InitializeComponent ();
		}

	    private void UpdateUI(EventTime newTime)
	    {
	        if (!_updatingUI)
	        {
	            _updatingUI = true;
	            StartDatePicker.Date = newTime.StartTime.Date;
	            StartTimePicker.Time = newTime.StartTime.TimeOfDay;
	            EndDatePicker.Date = newTime.EndTime.Date;
	            EndTimePicker.Time = newTime.EndTime.TimeOfDay;
	            DebugService.WriteLine("UI updated");
	            Task.Run(async () =>
	            {
	                await Task.Delay(50);
	                _updatingUI = false;
	            });
	        }
	    }

	    private static void OnEventTimeSlotPropertyChanged(BindableObject bindable, object oldValue, object newValue)
	    {
            if (bindable is EventDateTimePicker picker && newValue is EventTime newTime)
            {
                picker.UpdateUI(newTime);
            }
            //OnPropertyChanged(nameof(StartDate));
            //DebugService.WriteLine(bindable.GetType());
            //DebugService.WriteLine(oldValue.GetType());
            //DebugService.WriteLine($"old: {oldValue}"); 
            //DebugService.WriteLine($"new: {newValue}"); 
            DebugService.WriteLine("DateTime changed");
            

	    }
        
	}
}