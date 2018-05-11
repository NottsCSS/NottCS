using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NottCS.Converters;
using NottCS.Services;
using NottCS.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NottCS.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateEventPage3 : ContentPage
	{
	    private int _element = 0;

	    private Dictionary<int, int> _dateTimeIdentifier = new Dictionary<int, int>();
		public CreateEventPage3 ()
		{
		    BindingContext = new CreateEventViewModel3();
			InitializeComponent ();
		    InitializeTableViewSection2();
		}

	    private void InitializeTableViewSection2()
	    {
	        StackLayout startDateandTime = new StackLayout() { Orientation = StackOrientation.Horizontal};
	        startDateandTime.Children.Add(new Label() { Text = "Start: " });

            DatePicker startDate = new DatePicker();
	        _dateTimeIdentifier.Add(startDate.GetHashCode(), _element);
            startDate.PropertyChanged += DateTimePropertyChanged;
	        startDate.SetValue(DatePicker.DateProperty, DateTime.Today);
            startDateandTime.Children.Add(startDate);

	        TimePicker startTime = new TimePicker();
	        _dateTimeIdentifier.Add(startTime.GetHashCode(), _element + 1);
            startTime.PropertyChanged += DateTimePropertyChanged;
            startDateandTime.Children.Add(startTime);

            StackLayout endDateandTime = new StackLayout() { Orientation = StackOrientation.Horizontal };
	        endDateandTime.Children.Add(new Label() { Text = "End: " });

	        DatePicker endDate = new DatePicker();
	        _dateTimeIdentifier.Add(endDate.GetHashCode(), _element + 2);
            endDate.PropertyChanged += DateTimePropertyChanged;
            endDateandTime.Children.Add(endDate);

            TimePicker endTime = new TimePicker();
	        _dateTimeIdentifier.Add(endTime.GetHashCode(), _element + 3);
            endTime.PropertyChanged += DateTimePropertyChanged;
            endDateandTime.Children.Add(endTime);
//            
//
            ViewCell startDateandTimeCell = new ViewCell() { View = startDateandTime };
	        ViewCell endDateandTimeCell = new ViewCell() { View = endDateandTime };
	        Section2.Insert(Section2.Count - 1, startDateandTimeCell);
	        Section2.Insert(Section2.Count - 1, endDateandTimeCell);
	    }

	    private void DateTimePropertyChanged(object sender, EventArgs args)
	    {
	        var pickerId = _dateTimeIdentifier[sender.GetHashCode()];

	        Tuple<int, object> data = new Tuple<int, object>(pickerId, sender);

	        if (((CreateEventViewModel3)BindingContext).DateTimeChangedCommand.CanExecute(data))
	        {
	            ((CreateEventViewModel3)BindingContext).DateTimeChangedCommand.Execute(data);
            }
        }

	    private void ButtonPressed(object sender, EventArgs args)
	    {
	        if (((CreateEventViewModel3) BindingContext).BindingDateandTimeCommand.CanExecute(0))
	        {
	            ((CreateEventViewModel3) BindingContext).BindingDateandTimeCommand.Execute(null);
                DebugService.WriteLine("Added a new object to list");
	        }
	        
	        try
	        {
	            _element+=4;

	            StackLayout startDateandTime = new StackLayout() { Orientation = StackOrientation.Horizontal };
	            startDateandTime.Children.Add(new Label() { Text = "Start: " });

	            DatePicker startDate = new DatePicker();
	            _dateTimeIdentifier.Add(startDate.GetHashCode(), _element);
	            startDate.PropertyChanged += DateTimePropertyChanged;
	            startDateandTime.Children.Add(startDate);

	            TimePicker startTime = new TimePicker();
	            _dateTimeIdentifier.Add(startTime.GetHashCode(), _element + 1);
	            startTime.PropertyChanged += DateTimePropertyChanged;
	            startDateandTime.Children.Add(startTime);

	            StackLayout endDateandTime = new StackLayout() { Orientation = StackOrientation.Horizontal };
	            endDateandTime.Children.Add(new Label() { Text = "End: " });

	            DatePicker endDate = new DatePicker();
	            _dateTimeIdentifier.Add(endDate.GetHashCode(), _element + 2);
	            endDate.PropertyChanged += DateTimePropertyChanged;
	            endDateandTime.Children.Add(endDate);

	            TimePicker endTime = new TimePicker();
	            _dateTimeIdentifier.Add(endTime.GetHashCode(), _element + 3);
	            endTime.PropertyChanged += DateTimePropertyChanged;
	            endDateandTime.Children.Add(endTime);
	            //            
	            //
	            ViewCell startDateandTimeCell = new ViewCell() { View = startDateandTime };
	            ViewCell endDateandTimeCell = new ViewCell() { View = endDateandTime };
	            Section2.Insert(Section2.Count - 1, startDateandTimeCell);
	            Section2.Insert(Section2.Count - 1, endDateandTimeCell);
	        }
	        catch (Exception e)
	        {
	            DebugService.WriteLine(e);
                DebugService.WriteLine("List is not increased");
	        }

        }


    }
}