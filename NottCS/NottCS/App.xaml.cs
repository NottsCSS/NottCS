using System;
using Acr.UserDialogs;
using NottCS.Views;
using Xamarin.Forms;

namespace NottCS
{
	public partial class App : Application
	{

		public App ()
		{
			InitializeComponent();
            InitializeDialogService();
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

	    private void InitializeDialogService()
	    {
            
	    }
	}
}
