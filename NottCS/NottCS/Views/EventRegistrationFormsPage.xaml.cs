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
	public partial class EventRegistrationFormsPage : ContentPage
	{
		public EventRegistrationFormsPage ()
		{
			InitializeComponent ();
	        //AddImage(EmailContent, "NottCS.Images.Icons.EmailContent.png");
	        BindingContext = new EventRegistrationFormsViewModel();
	        //AddImage(BackIcon, "NottCS.Images.Icons.back.png");
	    }
	    private void AddImage(Image imageContainer, string imageLocation)
	    {
	        var assembly = typeof(NottCS.Views.EventRegistrationFormsPage);
	        if (imageContainer != null)
	        {
	            imageContainer.Source = ImageSource.FromResource(imageLocation, assembly);
	        }
	    }
    }
}