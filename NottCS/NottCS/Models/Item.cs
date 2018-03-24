using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using NottCS.ViewModels;

namespace NottCS.Models
{
    public class Item
    {
        public string ClubName { get; set; }
        public string EventName { get; set; }
        public string ImageURL { get; set; }
        public string Name { get; set; } = "default name";
        public string Entry { get; set; }
        public string Information { get; set; }
        public string FilledInformation { get; set; }
    }
}
