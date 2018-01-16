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
	public partial class EventRegSuccessPage : ContentPage
	{
		public EventRegSuccessPage ()
		{
			InitializeComponent ();
            AddImage(SuccessIcon, "NottCS.Images.Icons.okfixed.png");
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