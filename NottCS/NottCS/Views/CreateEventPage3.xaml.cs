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
	    public int element = 0;
		public CreateEventPage3 ()
		{
		    BindingContext = new CreateEventViewModel3();
			InitializeComponent ();
		    InitializeTableViewSection2();
		    AddDateorTime.Pressed += ButtonPressed;
		}

	    private void InitializeTableViewSection2()
	    {
	        StackLayout StartDateandTime = new StackLayout() { Orientation = StackOrientation.Horizontal };
	        StartDateandTime.Children.Add(new Label() { Text = "Start: " });
            DatePicker StartDate = new DatePicker();
            StartDate.SetBinding(DatePicker.DateProperty,new Binding("((CreateEventViewModel3)BindingContext).ListofDateandTime[element].StartDate",BindingMode.TwoWay,new DateTimeToTimeSpanConverter(),null,null));
	        StartDateandTime.Children.Add(StartDate);
	        TimePicker StartTime = new TimePicker();
	        StartTime.SetBinding(TimePicker.TimeProperty, new Binding("((CreateEventViewModel3)BindingContext).ListofDateandTime[element].StartTime", BindingMode.OneWay, new DateTimeToDateTimeOffsetConverter(), null, null));
	        StartDateandTime.Children.Add(StartTime);

            StackLayout EndDateandTime = new StackLayout() { Orientation = StackOrientation.Horizontal };
	        EndDateandTime.Children.Add(new Label() { Text = "End: " });
	        DatePicker EndDate = new DatePicker();
	        EndDate.SetBinding(DatePicker.DateProperty, new Binding("((CreateEventViewModel3)BindingContext).ListofDateandTime[element].EndDate", BindingMode.TwoWay, new DateTimeToTimeSpanConverter(), null, null));
	        EndDateandTime.Children.Add(EndDate);
	        TimePicker EndTime = new TimePicker();
	        EndTime.SetBinding(TimePicker.TimeProperty, new Binding("((CreateEventViewModel3)BindingContext).ListofDateandTime[element].EndTime", BindingMode.OneWay, new DateTimeToDateTimeOffsetConverter(), null, null));
	        EndDateandTime.Children.Add(EndTime);


            ViewCell StartDateandTimeCell = new ViewCell() { View = StartDateandTime };
	        ViewCell EndDateandTimeCell = new ViewCell() { View = EndDateandTime };
	        Section2.Insert(Section2.Count - 1, StartDateandTimeCell);
	        Section2.Insert(Section2.Count - 1, EndDateandTimeCell);
        }
	    private void ButtonPressed(object sender, EventArgs args)
	    {
	        if (((CreateEventViewModel3) BindingContext).BindingDateandTimeCommand.CanExecute(null))
	        {
	            ((CreateEventViewModel3) BindingContext).BindingDateandTimeCommand.Execute(null);
                DebugService.WriteLine("Added a new object to list");
	        }
	        
	        try
	        {
	            element++;
	            StackLayout StartDateandTime = new StackLayout() { Orientation = StackOrientation.Horizontal };
	            StartDateandTime.Children.Add(new Label() { Text = "Start: " });
	            DatePicker StartDate = new DatePicker();
	            StartDate.SetBinding(DatePicker.DateProperty, new Binding("((CreateEventViewModel3)BindingContext).ListofDateandTime[element].StartDate", BindingMode.TwoWay, new DateTimeToTimeSpanConverter(), null, null));
	            StartDateandTime.Children.Add(StartDate);
	            TimePicker StartTime = new TimePicker();
	            StartTime.SetBinding(TimePicker.TimeProperty, new Binding("((CreateEventViewModel3)BindingContext).ListofDateandTime[element].StartTime", BindingMode.OneWay, new DateTimeToDateTimeOffsetConverter(), null, null));
	            StartDateandTime.Children.Add(StartTime);

	            StackLayout EndDateandTime = new StackLayout() { Orientation = StackOrientation.Horizontal };
	            EndDateandTime.Children.Add(new Label() { Text = "End: " });
	            DatePicker EndDate = new DatePicker();
	            EndDate.SetBinding(DatePicker.DateProperty, new Binding("((CreateEventViewModel3)BindingContext).ListofDateandTime[element].EndDate", BindingMode.TwoWay, new DateTimeToTimeSpanConverter(), null, null));
	            EndDateandTime.Children.Add(EndDate);
	            TimePicker EndTime = new TimePicker();
	            EndTime.SetBinding(TimePicker.TimeProperty, new Binding("((CreateEventViewModel3)BindingContext).ListofDateandTime[element].EndTime", BindingMode.OneWay, new DateTimeToDateTimeOffsetConverter(), null, null));
	            EndDateandTime.Children.Add(EndTime);
            }
	        catch (Exception e)
	        {
	            DebugService.WriteLine(e);
                DebugService.WriteLine("List is not increased");
	        }

        }


    }
}