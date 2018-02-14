using System;
using Android.App;
using Microsoft.Identity.Client;
using NottCS.Views;
using NottCS.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Content;

[assembly: ExportRenderer(typeof(LoginPage), typeof(LoginPageRenderer))]
namespace NottCS.Droid
{
    public class LoginPageRenderer : PageRenderer
    {
        private LoginPage _page;

        public LoginPageRenderer(Context context) : base(context)
        {
            App.ClientApplication.RedirectUri = "msal81a5b712-2ec4-4d3f-9324-211f60d0a0c9://auth";
            App.UiParent = new UIParent(context as Activity);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            if (e == null) throw new ArgumentNullException(nameof(e));
            base.OnElementChanged(e);
            _page = e.NewElement as LoginPage;
            var activity = this.Context as Activity;

        }
    }
}