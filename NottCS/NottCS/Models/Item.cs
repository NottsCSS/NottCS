using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NottCS.Models
{
    public class Item
    {
        public string Name { get; set; } = "defaultName";
        public ImageSource IconImageSource { get; set; } = new UriImageSource(){Uri = new Uri("https://www.google.com/images/branding/googlelogo/2x/googlelogo_color_272x92dp.png") };
    }
}
