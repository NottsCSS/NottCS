using System;
using NottCS.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using NottCS.Services;
using NottCS.Validations;
using Xamarin.Forms;

namespace NottCS.ViewModels
{
    public class TestViewModel1 : BaseViewModel
    {
        public DateTime DateTime1 { get; set;} = DateTime.Now;

        public ICommand SomeCommand => new Command(SomeFunction);
        public ICommand AddCommand => new Command(AddItem);

        private void AddItem()
        {
            DateTime1 += new TimeSpan(1, 1, 22, 0);
        }
        private void SomeFunction()
        {
            DebugService.WriteLine("----------------");
            DebugService.WriteLine(DateTime1);

        }
    }
}
