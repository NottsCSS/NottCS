using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NottCS.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DateTimePicker : ContentView
    {
        public DateTimePicker()
        {
            InitializeComponent();
            PropertyChanged += OnPropertyChangedFunction;

            SelectedDate = SelectedDateTime.Date;
            SelectedTime = SelectedDateTime.TimeOfDay;
        }

        private bool _dateUpdated = false;
        private bool _timeUpdated = false;
        private bool _dateTimeUpdated = false;

        private void OnPropertyChangedFunction(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(SelectedDate):
                    if (_dateUpdated)
                        return;
                    Debug.WriteLine("Date change");
                    if (!_dateUpdated && !_timeUpdated && !_dateTimeUpdated) // this means this change is the source
                    {
                        Debug.WriteLine("Date change is source");
                        //therefore it will be in charge of resetting the states after everything is updated
                        try
                        {
                            //assume that date change would not affect time, so assume that time is already updated
                            _dateUpdated = true;
                            _timeUpdated = true;

                            SelectedDateTime = SelectedDate.Date.Add(SelectedTime);
                        }
                        finally
                        {
                            _dateUpdated = false;
                            _timeUpdated = false;
                            _dateTimeUpdated = false;
                            Debug.WriteLine("------------------------------------------------");
                        }
                    }
                    else //when it is not the source
                    {
                        //assumes that the updating is already called (in the setter)
                        _dateUpdated = true;
                    }
                    break;
                case nameof(SelectedTime):
                    if (_timeUpdated)
                        return;
                    Debug.WriteLine("Time change");
                    if (!_dateUpdated && !_timeUpdated && !_dateTimeUpdated) // this means this change is the source
                    {
                        Debug.WriteLine("Time change is source");
                        //therefore it will be in charge of resetting the states after everything is updated
                        try
                        {
                            //assume that time change would not affect date, so assume that date is already updated
                            _timeUpdated = true;
                            _dateUpdated = true;

                            SelectedDateTime = SelectedDateTime.Date.Add(SelectedTime);
                        }
                        finally
                        {
                            _dateUpdated = false;
                            _timeUpdated = false;
                            _dateTimeUpdated = false;
                            Debug.WriteLine("------------------------------------------------");
                        }
                    }
                    else //when it is not the source
                    {
                        //assumes that the updating is already called (in the setter)
                        _timeUpdated = true;
                    }
                    break;
                case nameof(SelectedDateTime):
                    if (_dateTimeUpdated) //to break recursion
                        return;

                    Debug.WriteLine("DateTime change");
                    if (!_dateUpdated && !_timeUpdated && !_dateTimeUpdated) // this means this change is the source
                    {
                        Debug.WriteLine("DateTime change is source");
                        //therefore it will be in charge of resetting the states after everything is updated
                        try
                        {
                            _dateTimeUpdated = true;
                            SelectedDate = SelectedDateTime.Date;
                            SelectedTime = SelectedDateTime.TimeOfDay;
                        }
                        finally
                        {
                            _dateUpdated = false;
                            _timeUpdated = false;
                            _dateTimeUpdated = false;
                            Debug.WriteLine("------------------------------------------------");

                        }
                    }
                    else //when it is not the source
                    {
                        _dateTimeUpdated = true;
                    }
                    break;
            }
        }

        public static readonly BindableProperty SelectedDateTimeProperty =
            BindableProperty.Create(nameof(SelectedDateTime), typeof(DateTime), typeof(DateTimePicker), DateTime.Now, BindingMode.TwoWay);

        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(DateTimePicker), "SLOT");


        private DateTime _selectedDate;
        private TimeSpan _selectedTime;


        public DateTime SelectedDateTime
        {
            get => (DateTime)GetValue(SelectedDateTimeProperty);
            set
            {
                SetValue(SelectedDateTimeProperty, value);
                OnPropertyChanged(nameof(SelectedDateTime));
            }
        }

        public DateTime SelectedDate
        {
            get => _selectedDate;
            set { _selectedDate = value; OnPropertyChanged(nameof(SelectedDate)); }
        }

        public TimeSpan SelectedTime
        {
            get => _selectedTime;
            set { _selectedTime = value; OnPropertyChanged(nameof(SelectedTime)); }
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
    }
}