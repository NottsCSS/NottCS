using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using NottCS.Models;

namespace NottCS.List
{
    class DummyListView
    {
        public static ObservableCollection<Item> DummyList { get; set; }
        static DummyListView()
        {
            DummyList = new ObservableCollection<Item>();
            DummyList.Add(new Item
            {
                Title = "I'm just a title",
                ImageURL = "http://icons.iconarchive.com/icons/graphicloads/100-flat/24/home-icon.png"
            });
            DummyList.Add(new Item
            {

            });
            DummyList.Add(new Item
            {

            });
            DummyList.Add(new Item
            {

            });
            DummyList.Add(new Item
            {

            });

        }
    }
}
