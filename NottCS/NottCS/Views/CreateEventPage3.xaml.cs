using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
		    //AddDateorTime.Pressed += ButtonPressed;
		}

	    private void InitializeTableViewSection2()
	    {
	        StackLayout startDateandTime = new StackLayout() { Orientation = StackOrientation.Horizontal };
	        startDateandTime.Children.Add(new Label() { Text = "Start: " });

            DatePicker startDate = new DatePicker();
	        startDate.PropertyChanged += DateTimePropertyChanged;
	        startDateandTime.Children.Add(startDate);
	        _dateTimeIdentifier.Add(startDate.GetHashCode(), _element);

	        TimePicker startTime = new TimePicker();
	        startTime.PropertyChanged += DateTimePropertyChanged;
            startDateandTime.Children.Add(startTime);
	        _dateTimeIdentifier.Add(startTime.GetHashCode(), _element+1);

            StackLayout endDateandTime = new StackLayout() { Orientation = StackOrientation.Horizontal };
	        endDateandTime.Children.Add(new Label() { Text = "End: " });

	        DatePicker endDate = new DatePicker();
	        endDate.PropertyChanged += DateTimePropertyChanged;
            endDateandTime.Children.Add(endDate);
	        _dateTimeIdentifier.Add(endDate.GetHashCode(), _element+2);

            TimePicker endTime = new TimePicker();
	        endTime.PropertyChanged += DateTimePropertyChanged;
            endDateandTime.Children.Add(endTime);
	        _dateTimeIdentifier.Add(endTime.GetHashCode(), _element+3);
            

            ViewCell startDateandTimeCell = new ViewCell() { View = startDateandTime };
	        ViewCell endDateandTimeCell = new ViewCell() { View = endDateandTime };
	        Section2.Insert(Section2.Count - 1, startDateandTimeCell);
	        Section2.Insert(Section2.Count - 1, endDateandTimeCell);
        }

	    private void DateTimePropertyChanged(object sender, EventArgs args)
	    {
	        var pickerId = _dateTimeIdentifier[sender.GetHashCode()];
	        var index = pickerId / 4; //assume this is floored

	        TimeSpan val;
            DateTime val1;
	        switch (pickerId % 4)
	        {
                case 0: //Start Date
                    val1 = ((DatePicker) sender).Date;
                    break;
                case 1: //Start Time
                    val = ((TimePicker)sender).Time;
                    break;
	            case 2: //End Date
	                val1 = ((DatePicker)sender).Date;
	                break;
	            case 3: //End Time
	                val = ((TimePicker)sender).Time;
	                break;
            }




            
	        if (((CreateEventViewModel3)BindingContext).DateTimeChangedCommand.CanExecute(args))
	        {
	            ((CreateEventViewModel3)BindingContext).DateTimeChangedCommand.Execute(args);
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
	            startDate.PropertyChanged += DateTimePropertyChanged;
	            startDateandTime.Children.Add(startDate);
	            _dateTimeIdentifier.Add(startDate.GetHashCode(), _element);

	            TimePicker startTime = new TimePicker();
	            startTime.PropertyChanged += DateTimePropertyChanged;
	            startDateandTime.Children.Add(startTime);
	            _dateTimeIdentifier.Add(startTime.GetHashCode(), _element+1);

	            StackLayout endDateandTime = new StackLayout() { Orientation = StackOrientation.Horizontal };
	            endDateandTime.Children.Add(new Label() { Text = "End: " });

	            DatePicker endDate = new DatePicker();
	            endDate.PropertyChanged += DateTimePropertyChanged;
	            endDateandTime.Children.Add(endDate);
	            _dateTimeIdentifier.Add(endDate.GetHashCode(), _element+2);

	            TimePicker endTime = new TimePicker();
	            endTime.PropertyChanged += DateTimePropertyChanged;
	            endDateandTime.Children.Add(endTime);
	            _dateTimeIdentifier.Add(endTime.GetHashCode(), _element+3);


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