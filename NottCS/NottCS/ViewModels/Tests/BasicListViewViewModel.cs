using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace NottCS.ViewModels.Tests
{
    public class BasicListViewViewModel : BaseViewModel
    {
        public ObservableCollection<string> NameList { get; set; } = new ObservableCollection<string>()
        {
            "ASD", "QWE"
        };

        private int i = 0;
        public ICommand AddItemCommand => new Command(() => NameList.Add($"Name {i++}"));
        public ICommand DeleteCommand => new Command<string>(DeleteItem);

        private void DeleteItem(string item)
        {
            NameList.Remove(item);
        }
    }
}
