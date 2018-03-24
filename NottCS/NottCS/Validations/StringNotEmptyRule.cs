using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("NottCSTest")]
namespace NottCS.Validations
{

    internal class StringNotEmptyRule : IValidationRule<string>
    {
        public string ValidationMessage { get;} = "cannot be empty";

        public bool Check(string value)
        {
            if (value == null)
            {
                return false;
            }
            
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}
