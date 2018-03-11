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
    public partial class ClubPage : TabbedPage
    {
        public ClubPage ()
        {
            InitializeComponent();
            BindingContext = new ClubViewModel();
            AddImage(Logo1, "NottCS.Images.Icons.icon2.png");
            AddImage(PlaceHolder, "NottCS.Images.example-background.jpg");
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