using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;
using System.Text;

namespace NottCS.ViewModels
{
    class ListViewModel:BaseViewModel
    {
        public string ClubName { get; set; }
        public string ImageURL { get; set; }

       
        
        public static ObservableCollection<ListViewModel> DummyList { get; set; }
        static ListViewModel()
        {
            DummyList = new ObservableCollection<ListViewModel>();
            DummyList.Add(new ListViewModel
            {
                Title = "I'm just a title",
                ImageURL = "http://icons.iconarchive.com/icons/graphicloads/100-flat/24/home-icon.png"
            });
            DummyList.Add(new ListViewModel
            {

            });
            DummyList.Add(new ListViewModel
            {

            });
            DummyList.Add(new ListViewModel
            {

            });
            DummyList.Add(new ListViewModel
            {

            });

        }
    }
}
