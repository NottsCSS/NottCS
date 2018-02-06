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
