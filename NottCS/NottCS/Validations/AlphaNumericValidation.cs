using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace NottCS.ViewModels.Validations
{
    class AlphaNumericValidation<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }
            var str = value as string;
            Regex r = new Regex("^[a-zA-Z0-9]*$");
            return r.IsMatch(str);
        }
    }
}
