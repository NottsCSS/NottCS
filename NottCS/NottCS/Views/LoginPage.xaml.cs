using System;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NottCS.Services;
using NottCS.ViewModels;

namespace NottCS.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
	    private LoginViewModel viewModel;
		public LoginPage ()
		{
			InitializeComponent ();
		    BindingContext = viewModel = new LoginViewModel();
		    //var assembly = typeof(NottCS.Views.LoginPage);
            //if (IconOne != null) IconOne.Source = ImageSource.FromResource("NottCS.Images.Icons.icon2.png", assembly);
            AddImage(IconOne, "NottCS.Images.Icons.icon2.png");
            AddImage(BannerImage, "NottCS.Images.example-background.jpg");
        }

        /// <summary>
        /// Applies image to the image container
        /// </summary>
        /// <param name="imageContainer">Name of the image container</param>
        /// <param name="imageLocation">Image file location</param>
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