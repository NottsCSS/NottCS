using System;
using System.Collections.Generic;
using System.Text;

namespace NottCS.Models
{
    public enum MenuItemType
    {
        Browse,
        About
    }
    public class HomeMenuItem1
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
