using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Microsoft.Identity.Client;
using NottCS.Services.Navigation;
using NottCS.Views;
using Xamarin.Forms;
using NottCS.Models;

namespace NottCS
{
	public partial class App : Application
	{
	    public static PublicClientApplication ClientApplication { get; private set; }
	    public static readonly string[] Scopes = { "User.Read" };
	    public static UIParent UiParent = null;
        public static AuthenticationResult MicrosoftAuthenticationResult = null;
        public App ()
		{
			InitializeComponent();
            InitializeDialogService();
		    ClientApplication = new PublicClientApplication("81a5b712-2ec4-4d3f-9324-211f60d0a0c9");
		    ClientApplication.RedirectUri = "msal81a5b712-2ec4-4d3f-9324-211f60d0a0c9://auth";
            MainPage = new NavigationPage(new EventPage());
        }

	    private static Task InitNavigation()
	    {
	        return NavigationService.InitializeAsync();
	    }

        protected override async void OnStart ()
		{
            // Handle when your app starts
		    Stopwatch stopwatch = new Stopwatch();
		    stopwatch.Start();
//            await InitNavigation();
		    Debug.WriteLine($"Init navigation took {stopwatch.ElapsedMilliseconds}ms");
            stopwatch.Stop();

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
