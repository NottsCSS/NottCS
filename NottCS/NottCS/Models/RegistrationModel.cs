using NottCS.Validations;

namespace NottCS.Models
{
    internal class RegistrationModel
    {
        public RegistrationModel()
        {
        }
        public ValidatableObject<string> Name { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> OWA { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> StudentID { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Password { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> LibraryNumber { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Course { get; set; } = new ValidatableObject<string>();

    }

}
