using NottCS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace NottCS.ViewModels
{
    public class TestViewModel : BaseViewModel
    {

        public ObservableCollection<Dummy> DummyListCollection { get; set; } = new ObservableCollection<Dummy>
        {
            new Dummy() {Name = "some name1"},
            new Dummy() {Name = "some name 2"},
            new Dummy() {Name = "some Name 3"},
            new Dummy() {Name = "sasd name 4"},
            new Dummy() {Name = "CYKA NAME 5"}
        };

        public ObservableCollection<Item> ClubList { get; set; } = new ObservableCollection<Item>
        {
            new Item()
            {
                ClubName = "Name 1",
                ImageURL = "https://www.google.com/images/branding/googlelogo/2x/googlelogo_color_272x92dp.png"
            },
            new Item()
            {
                ClubName = "Name 2",
                ImageURL =
                    "https://cloud.netlifyusercontent.com/assets/344dbf88-fdf9-42bb-adb4-46f01eedd629/68dd54ca-60cf-4ef7-898b-26d7cbe48ec7/10-dithering-opt.jpg"
            },
            new Item()
            {
                ClubName = "Club name 3",
                ImageURL =
                    "https://beebom-redkapmedia.netdna-ssl.com/wp-content/uploads/2016/01/Reverse-Image-Search-Engines-Apps-And-Its-Uses-2016.jpg"
            }
        };

        public ICommand ItemTappedCommand => new Command(ItemTapped);
        private void ItemTapped()
        {
            Debug.WriteLine("Item Tapped");
        }
    }
}
