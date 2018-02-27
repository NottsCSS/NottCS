﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using DLToolkit.Forms.Controls;
using Xamarin.Forms;

namespace NottCS.Converters
{
    class SelectedItemEventArgsToSelectedItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null && parameter is ListView)
                if (((ListView)parameter).SelectedItem != null)
                    ((ListView)parameter).SelectedItem = null;
            if (parameter != null && parameter is FlowListView)
                if (((FlowListView)parameter).SelectedItem != null)
                    ((FlowListView)parameter).SelectedItem = null;

            var eventArgs = value as SelectedItemChangedEventArgs;
            return eventArgs?.SelectedItem;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
