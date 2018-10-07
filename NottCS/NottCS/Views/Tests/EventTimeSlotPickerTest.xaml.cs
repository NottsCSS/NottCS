using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NottCS.ViewModels.Tests;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NottCS.Views.Tests
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EventTimeSlotPickerTest : ContentPage
	{
		public EventTimeSlotPickerTest ()
		{
			InitializeComponent ();
		    BindingContext = new EventTimeSlotPickerTestViewModel();
		}
	}
}