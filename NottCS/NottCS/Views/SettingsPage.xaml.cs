using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NottCS.ViewModels;
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