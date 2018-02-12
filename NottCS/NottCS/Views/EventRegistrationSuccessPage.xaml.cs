using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NottCS.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NottCS.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EventRegistrationSuccessPage : ContentPage
	{
		public EventRegistrationSuccessPage()
		{
			InitializeComponent ();
            AddImage(SuccessIcon, "NottCS.Images.Icons.okfixed.png");
		    BindingContext = new EventRegistrationSuccessViewModel();
            //AddImage(Back, "NottCS.Images.Icons.back.png");
        }

        private void AddImage(Image imageContainer, string imageLocation)
        {
            var assembly = typeof(NottCS.Views.LoginPage);
            if (imageContainer != null)
            {
                imageContainer.Source = ImageSource.FromResource(imageLocation, assembly);
            }
        }
    }
}