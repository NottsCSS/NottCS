using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Microsoft.Identity.Client;
using NottCS.Services.Navigation;
using NottCS.Views;
using Xamarin.Forms;

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
		    Stopwatch stopwatch = new Stopwatch();
            if (Device.RuntimePlatform == Device.UWP)
		    {
                stopwatch.Start();
		        InitNavigation();
                Debug.WriteLine($"Init navigation took {stopwatch.ElapsedMilliseconds}ms");
		    }
            stopwatch.Stop();
            //            MainPage = new NavigationPage(new LoginPage());
        }

	    private static Task InitNavigation()
	    {
	        return NavigationService.InitializeAsync();
	    }

        protected override async void OnStart ()
		{
            // Handle when your app starts

		    if (Device.RuntimePlatform != Device.UWP)
		    {
		        await InitNavigation();
		    }

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
