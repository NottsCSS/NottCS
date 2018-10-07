using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;

namespace NottCS.ViewModels.Tests
{
    public class DateTimePickerTestViewModel : BaseViewModel
    {
        private DateTime _selectedDateTime = DateTime.Now;

        public DateTimePickerTestViewModel()
        {
            PropertyChanged += (sender, args) =>
            {
                Debug.WriteLine($"Sender: {sender}, PropertyName: {args.PropertyName}");
            };
        }

        public DateTime SelectedDateTime
        {
            get => _selectedDateTime;
            set => SetProperty(ref _selectedDateTime, value);
        }

        public ICommand DebugCommand => new Xamarin.Forms.Command(() => SelectedDateTime = SelectedDateTime.AddHours(3.2));
    }
}
