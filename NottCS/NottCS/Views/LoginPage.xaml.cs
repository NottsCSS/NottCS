using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NottCS.Services;
using NottCS.ViewModels;

namespace NottCS.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
	    private LoginViewModel viewModel;
		public LoginPage ()
		{
			InitializeComponent ();
		    BindingContext = viewModel = new LoginViewModel();
            if (IconOne != null) IconOne.Source = ImageSource.FromResource("NottCS.Icons.icon2.png");
            
        }
	}
}