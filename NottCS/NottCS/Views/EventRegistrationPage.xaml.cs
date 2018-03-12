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
	public partial class EventRegistrationPage : ContentPage
	{
		public EventRegistrationPage()
		{
			InitializeComponent ();
            AddImage(EmailContent, "NottCS.Images.Icons.EmailContent.png");
		    BindingContext =new EventRegistrationViewModel();
		    //AddImage(BackIcon, "NottCS.Images.Icons.back.png");
		}
        private void AddImage(Image imageContainer, string imageLocation)
        {
            var assembly = typeof(NottCS.Views.EventRegistrationPage);
            if (imageContainer != null)
            {
                imageContainer.Source = ImageSource.FromResource(imageLocation, assembly);
            }
        }
    }
}