using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using NottCS.Models;
using Xamarin.Forms;

namespace NottCS.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private string _label = "Hello";

        public ObservableCollection<Item> ClubList { get; set; } = new ObservableCollection<Item>
        {
            new Item()
            {
                ClubName = "Name 1",
                ImageURL = "https://www.google.com/images/branding/googlelogo/2x/googlelogo_color_272x92dp.png"
            },
            new Item() {
                ClubName = "Name 2",
                ImageURL = "https://cloud.netlifyusercontent.com/assets/344dbf88-fdf9-42bb-adb4-46f01eedd629/68dd54ca-60cf-4ef7-898b-26d7cbe48ec7/10-dithering-opt.jpg"
            },
            new Item()
            {
                ClubName = "Club name 3",
                ImageURL = "https://beebom-redkapmedia.netdna-ssl.com/wp-content/uploads/2016/01/Reverse-Image-Search-Engines-Apps-And-Its-Uses-2016.jpg"
            }
        };

        public List<string> ClubTypePickList { get; set; } = new List<string>{"My Clubs Only", "All Clubs", "Favourite Clubs"};

        public string Label
        {
            get => _label;
            set => SetProperty(ref _label, value);
        }

        private int Count { get; set; } = 0;
        public ICommand Button1Command => new Command(ChangeLabel);
        

        public string SelectedClubType { get; set; }

        private void ChangeLabel()
        {
            Label = $"Hello World {Count}";
            Count++;
            Debug.WriteLine("Button pressed");
        }

        public HomeViewModel()
        {
            SelectedClubType = ClubTypePickList[0];
        }
    }
}
