using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("NottCSTest")]
namespace NottCS.Validations
{
    internal class StringAlphaNumericRule : IValidationRule<string>
    {
        public string ValidationMessage { get;} = "must contain only alphanumeric characters";

        public bool Check(string value)
        {
            if (value == null)
            {
                return false;
            }
            var str = value;
            Regex r = new Regex("^[a-zA-Z0-9 ]*$");
            return r.IsMatch(str);
        }
    }
}
