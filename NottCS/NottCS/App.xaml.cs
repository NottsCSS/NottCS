﻿using System;
using Microsoft.Identity.Client;
using NottCS.Views;
using Xamarin.Forms;

namespace NottCS
{
	public partial class App : Application
	{
	    public static PublicClientApplication ClientApplication { get; private set; }
	    public static readonly string[] Scopes = { "User.Read" };
        public App ()
		{
			InitializeComponent();
		    ClientApplication = new PublicClientApplication("81a5b712-2ec4-4d3f-9324-211f60d0a0c9");
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
	}
}
