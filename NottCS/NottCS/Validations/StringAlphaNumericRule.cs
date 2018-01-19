using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("NottCSTest")]
namespace NottCS.Validations
{
    internal class StringAlphaNumericRule : IValidationRule<string>
    {
        public string ValidationMessage { get; set; }

        public bool Check(string value)
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
