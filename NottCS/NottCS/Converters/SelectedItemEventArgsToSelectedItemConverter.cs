using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using DLToolkit.Forms.Controls;
using NottCS.Services;
using Xamarin.Forms;

namespace NottCS.Converters
{
    class SelectedItemEventArgsToSelectedItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DebugService.WriteLine("Selected item converter called");
            if (parameter != null && parameter is ListView)
            {

                if (Device.RuntimePlatform == Device.UWP)
                {
                    Device.BeginInvokeOnMainThread(() => ((ListView) parameter).SelectedItem = null);
                }
                else
                {
                    if (((ListView) parameter).SelectedItem != null)
                    {
                        ((ListView) parameter).SelectedItem = null;
                        DebugService.WriteLine("Selected item set to null");
                    }
                }
            }

            if (parameter != null && parameter is FlowListView)
            {

                if (Device.RuntimePlatform == Device.UWP)
                {
                    Device.BeginInvokeOnMainThread(() => ((FlowListView)parameter).SelectedItem = null);
                }
                else
                {
                    if (((FlowListView)parameter).SelectedItem != null)
                    {
                        ((FlowListView)parameter).SelectedItem = null;
                        DebugService.WriteLine("Selected item set to null");
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
