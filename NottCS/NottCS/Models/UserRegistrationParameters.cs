using NottCS.Validations;

namespace NottCS.Models
{
    internal class UserRegistrationParameters
    {
        public UserRegistrationParameters()
        {
            StudentID.Validations.Add(new StringNumericRule());
            StudentID.Validations.Add(new StringNotEmptyRule());
            LibraryNumber.Validations.Add(new StringNumericRule());
            LibraryNumber.Validations.Add(new StringNotEmptyRule());
            Course.Validations.Add(new StringNotEmptyRule());
        }
        public ValidatableObject<string> StudentID { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> LibraryNumber { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Course { get; set; } = new ValidatableObject<string>();

    }

}
