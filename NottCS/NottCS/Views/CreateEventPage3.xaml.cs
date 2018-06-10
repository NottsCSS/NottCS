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
		public CreateEventPage3 ()
		{
		    BindingContext = new CreateEventViewModel3();
			InitializeComponent ();
		    SomeList.BindingContext = this.BindingContext;
		}
        

    }
}