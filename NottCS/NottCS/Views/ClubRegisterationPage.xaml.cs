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
	public partial class ClubRegisterationPage : ContentPage
	{
		public ClubRegisterationPage ()
		{
			InitializeComponent ();
            AddImage(PlaceHolder, "NottCS.Images.example-background.jpg");
            AddImage(Logo1, "NottCS.Images.Icons.icon2.png");
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