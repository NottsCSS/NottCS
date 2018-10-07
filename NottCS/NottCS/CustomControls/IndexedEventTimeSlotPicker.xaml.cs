using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NottCS.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NottCS.CustomControls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IndexedEventTimeSlotPicker : ContentView
	{
		public IndexedEventTimeSlotPicker ()
		{
			InitializeComponent ();
		}

	    public static readonly BindableProperty EventTimeListProperty =
	        BindableProperty.Create(nameof(EventTimeList), typeof(ObservableCollection<EventTime>), typeof(IndexedEventTimeSlotPicker), defaultBindingMode: BindingMode.TwoWay, defaultValue: new ObservableCollection<EventTime>());

	    public ObservableCollection<EventTime> EventTimeList
	    {
	        get => (ObservableCollection<EventTime>) GetValue(EventTimeListProperty);
	        set => SetValue(EventTimeListProperty, value);
	    }
    }
}