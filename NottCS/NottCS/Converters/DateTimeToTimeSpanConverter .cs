using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace NottCS.Converters
{
    public class DateTimeToTimeSpanConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                DateTime dt = (DateTime)value;
                //Get the timespan from subtracting the date from the original DateTime
                //this returns a timespan representing the time component of the DateTime
                TimeSpan ts = dt - dt.Date;
                return ts;
            }
            catch (Exception)
            {
                return TimeSpan.MinValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //It just doesn't make sense to convert back to a datetime.
            //There is no concept representation of date in the incoming TimeSpan value. 
            throw new NotImplementedException();
        }
    }
}
