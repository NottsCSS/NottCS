using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NottCS.Validations
{
    class DigitsRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }
            var str = value as string;
            Regex r = new Regex("^[0-9]*$");
            return r.IsMatch(str);
        }
    }
}
