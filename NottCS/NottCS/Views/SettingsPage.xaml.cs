using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NottCS.ViewModels;
using System.Diagnostics;
using NottCS.Services;

namespace NottCS.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage ()
		{
			InitializeComponent ();
            DebugService.WriteLine("Attempting to call ViewModel");
		    BindingContext = new SettingsViewModel();

		}
    }
}