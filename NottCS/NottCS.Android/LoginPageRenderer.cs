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
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            if (e == null) throw new ArgumentNullException(nameof(e));
            base.OnElementChanged(e);
            _page = e.NewElement as LoginPage;
            
        }
    }
}