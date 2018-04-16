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
    public partial class HomePage : TabbedPage
    {
        public HomePage ()
        {
            InitializeComponent();
            BindingContext = new HomeViewModel();
            if (Device.RuntimePlatform == Device.UWP)
            {
                ToolbarItem.IsEnabled = true;
            }
            else if (Device.RuntimePlatform != Device.UWP)
            {
                ToolbarItem.IsEnabled = false;
            }
            //AddImage(IconOne, "NottCS.Images.Icons.icon2.png");
            //AddImage(BannerImage, "NottCS.Images.example-background.jpg");
        }
        private void AddImage(Image imageContainer, string imageLocation)
        {
            var assembly = typeof(NottCS.Views.HomePage);
            if (imageContainer != null)
            {
                imageContainer.Source = ImageSource.FromResource(imageLocation, assembly);
            }
        }
    }
}