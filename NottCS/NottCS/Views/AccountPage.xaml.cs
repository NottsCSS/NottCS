using NottCS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NottCS.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AccountPage : ContentPage
	{
		public AccountPage ()
		{
            BindingContext = new AccountViewModel();
			InitializeComponent ();
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
            var assembly = typeof(NottCS.Views.AccountPage);
            if (imageContainer != null)
            {
                imageContainer.Source = ImageSource.FromResource(imageLocation, assembly);
            }
        }
    }
}