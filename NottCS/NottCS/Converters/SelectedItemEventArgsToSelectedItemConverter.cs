using System;
using System.Diagnostics;
using System.Globalization;
using Xamarin.Forms;

namespace NottCS.Converters
{
    internal class SelectedItemEventArgsToSelectedItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.WriteLine("Selected item converter called");
            if (parameter != null && parameter is ListView)
            {

                if (Device.RuntimePlatform == Device.UWP)
                {
                    Device.BeginInvokeOnMainThread(() => ((ListView)parameter).SelectedItem = null);
                }
                else
                {
                    if (((ListView)parameter).SelectedItem != null)
                    {
                        ((ListView)parameter).SelectedItem = null;
                        Debug.WriteLine("Selected item set to null");
                    }
                }
            }
            var eventArgs = value as SelectedItemChangedEventArgs;
            return eventArgs?.SelectedItem;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}