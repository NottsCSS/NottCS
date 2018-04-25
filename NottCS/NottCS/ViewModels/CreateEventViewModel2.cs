using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NottCS.Services;
using Xamarin.Forms;

namespace NottCS.ViewModels
{
    internal class CreateEventViewModel2 : BaseViewModel
    {
        private double _height1 = 180;
        private ObservableCollection<KeyValuePair<string, string>> _dataList = new ObservableCollection<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("Something", "Thing 2")
        };

        private string _someString = "some string??";

        public string SomeString
        {
            get => _someString;
            set => SetProperty(ref _someString, value);
        }

        public double Height1
        {
            get => _height1;
            set => SetProperty(ref _height1, value);
        }

        public ObservableCollection<KeyValuePair<string, string>> DataList
        {
            get => _dataList;
            set => SetProperty(ref _dataList, value);
        }

        public ICommand TappedCommand => new Command(async () => await TappedTask());

        private Task TappedTask()
        {
            DataList.Add(new KeyValuePair<string, string>("What", "HOWWWW"));
            Height1 = (DataList.Count + 1) * 60;
            DebugService.WriteLine($"Requested height: {Height1}");
            return Task.FromResult(false);
        }
    }
}