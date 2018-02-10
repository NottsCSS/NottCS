using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NottCS.ViewModels;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace NottCS.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage ()
		{
			InitializeComponent ();
            Debug.WriteLine("Attempting to call ViewModel");
            BindingContext = new SettingsViewModel();
            
        }
	}
}