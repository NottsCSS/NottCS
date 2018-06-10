using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NottCS.Models;
using NottCS.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NottCS.CustomControls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EventDateTimePicker : ContentView
	{
	    public static readonly BindableProperty TitleProperty =
	        BindableProperty.Create(nameof(Title), typeof(string), typeof(EventDateTimePicker), "Default Title", BindingMode.OneWay);

	    public static readonly BindableProperty EventTimeSlotProperty =
	        BindableProperty.Create(nameof(EventTimeSlot), typeof(EventTime), typeof(EventDateTimePicker),
	            new EventTime()
	            {
	                StartTime = DateTime.Now,
	                EndTime = DateTime.Now + new TimeSpan(1, 3, 30, 0),
	                Id = "0",
	                Event = "0"
	            }, BindingMode.TwoWay);

	    public EventTime EventTimeSlot
	    {
	        get
	        {
	            StackTrace stackTrace = new StackTrace();           // get call stack
	            StackFrame[] stackFrames = stackTrace.GetFrames();  // get method calls (frames)
	            if (stackFrames != null) DebugService.WriteLine(stackFrames[1].GetMethod().Name);
	            DebugService.WriteLine("EventTime getter called");
	            return (EventTime)GetValue(EventTimeSlotProperty);
            } 
	        set
	        {
	            DebugService.WriteLine("EventTime setter called");
                SetValue(EventTimeSlotProperty, value);
	            DebugService.WriteLine("On start date property changed");
                OnPropertyChanged("StartDate");
	            OnPropertyChanged("EndDate");
	            OnPropertyChanged("StartTime");
	            OnPropertyChanged("EndTime");
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
                StackTrace stackTrace = new StackTrace();           // get call stack
                StackFrame[] stackFrames = stackTrace.GetFrames();  // get method calls (frames)
                if (stackFrames != null)
                    foreach (var frame in stackFrames)
                    {
                        DebugService.WriteLine(frame);
                    }

                DebugService.WriteLine("StartDate getter called");
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

	            DebugService.WriteLine("setting event time");
                EventTimeSlot = newEventTime;
                
                DebugService.WriteLine("StartDate setter called");
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
	            EventTimeSlot = newEventTime;
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
	            EventTimeSlot = newEventTime;
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
	        }
	    }

        public ICommand PrintDataCommand => new Command(PrintData);
	    private void PrintData()
        {
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

    }
}