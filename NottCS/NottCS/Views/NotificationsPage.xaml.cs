using NottCS.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;

namespace NottCS.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NotificationsPage : ContentPage
	{
		public NotificationsPage ()
		{
			InitializeComponent ();
		    Debug.WriteLine("Attempting to call ViewModel");
            BindingContext = new NotificationsViewModel();
		}
	}
}