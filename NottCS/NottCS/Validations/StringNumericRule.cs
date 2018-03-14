using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

[assembly: InternalsVisibleTo("NottCSTest")]
namespace NottCS.Validations
{
    internal class StringNumericRule : IValidationRule<string>
    {
        public string ValidationMessage { get;} = "must contain only numbers";

        public bool Check(string value)
        {
            if (value == null)
            {
                return false;
            }
            var str = value;
            Regex r = new Regex("^[0-9]*$");
            return r.IsMatch(str);
        }
    }
}
