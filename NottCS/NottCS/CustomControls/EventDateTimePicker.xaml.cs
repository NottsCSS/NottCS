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
        public static readonly BindableProperty TitleProperty =
	        BindableProperty.Create(nameof(Title), typeof(string), typeof(EventDateTimePicker), defaultBindingMode:BindingMode.TwoWay, defaultValue: "Default Title");

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
            }
	    }
        public string Title
	    {
	        get => (string)GetValue(TitleProperty);
	        set => SetValue(TitleProperty, value);
	    }
        
	    public DateTime StartDate
	    {
	        get
            {

                return EventTimeSlot.StartTime.Date;
            }
	        set
	        {
	            var oldStartTime = EventTimeSlot.StartTime;
	            EventTimeSlot.StartTime = new DateTime(value.Year, value.Month, value.Day, oldStartTime.Hour,
	                oldStartTime.Minute,
	                oldStartTime.Second);
	        }
	    }

	    public TimeSpan StartTime
	    {
	        get => EventTimeSlot.StartTime.TimeOfDay;
	        set => EventTimeSlot.StartTime = EventTimeSlot.StartTime.Date + value;
	    }
	    public DateTime EndDate
	    {
	        get => EventTimeSlot.EndTime.Date;
	        set
	        {
	            var oldEndTime = EventTimeSlot.EndTime;
	            EventTimeSlot.EndTime = new DateTime(value.Year, value.Month, value.Day, oldEndTime.Hour,
	                oldEndTime.Minute,
	                oldEndTime.Second);
            }
	    }

	    public TimeSpan EndTime
	    {
	        get => EventTimeSlot.EndTime.TimeOfDay;
	        set => EventTimeSlot.EndTime = EventTimeSlot.EndTime.Date + value;
        }

        public ICommand PrintDataCommand => new Command(PrintData);
	    private void PrintData()
	    {
	        Title = Title + "1";
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
	                await Task.Delay(100);
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
            DebugService.WriteLine("DateTime changed");
            

	    }
        
	}
}