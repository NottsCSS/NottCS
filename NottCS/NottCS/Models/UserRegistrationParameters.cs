using NottCS.Validations;

namespace NottCS.Models
{
    internal class UserRegistrationParameters
    {
        public UserRegistrationParameters()
        {
        }
        public ValidatableObject<string> StudentID { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> LibraryNumber { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Course { get; set; } = new ValidatableObject<string>();

    }

}
