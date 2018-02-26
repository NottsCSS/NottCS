using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;
using NottCS.iOS;
using NottCS.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(LoginPage), typeof(LoginPageRenderer))]
namespace NottCS.iOS
{
    class LoginPageRenderer : PageRenderer
    {
        LoginPage page;
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            page = e.NewElement as LoginPage;
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }
    }
}