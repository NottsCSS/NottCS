using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Reflection;
using NottCS.Services;

namespace NottCS.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page1 : ContentPage
	{
		public Page1 ()
		{
			InitializeComponent ();
		    // ...
		    // NOTE: use for debugging, not in released app code!
		    if (iconOne != null) iconOne.Source = ImageSource.FromResource("NottCS.Icons.logo1.jpeg");
		    if (iconTwo != null) iconTwo.Source = ImageSource.FromResource("NottCS.Icons.icon2.png");

            var assembly = typeof(Page1).GetTypeInfo().Assembly;
		    foreach (var res in assembly.GetManifestResourceNames())
		    {
		        System.Diagnostics.Debug.WriteLine("found resource: " + res);
		    }
        }
	}
}