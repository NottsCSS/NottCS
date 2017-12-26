using System;
using System.Collections.Generic;
using System.Text;
using NottCS.Validations;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace NottCS.ViewModels
{
    class RegistrationViewModel : BaseViewModel
    {
        #region PublicMethodsWithPrivateBackingFields
        private bool _isValidName = true;
        private bool _isValidOWA = true;
        private bool _isValidStudentId = true;
        private bool _isValidPassword = true;
        private bool _isValidLibraryNumber = true;
        public bool IsValidName
        {
            get => _isValidName;
            set => SetProperty(ref _isValidName, value);
        }
        public bool IsValidOWA
        {
            get => _isValidOWA;
            set => SetProperty(ref _isValidOWA, value);
        }
        public bool IsValidStudentID
        {
            get => _isValidStudentId;
            set => SetProperty(ref _isValidStudentId, value);
        }
        public bool IsValidPassword
        {
            get => _isValidPassword;
            set => SetProperty(ref _isValidPassword, value);
        }
        public bool IsValidLibraryNumber
        {
            get => _isValidLibraryNumber;
            set => SetProperty(ref _isValidLibraryNumber, value);
        }
#endregion
        public ValidatableObject<string> Name { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> OWA { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> StudentID { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Password { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> LibraryNumber { get; set; } = new ValidatableObject<string>();

        public RegistrationViewModel()
        {
            Title = "NottCS Registration";
            AddValidationRules();
        }
        public ICommand RegisterCommand => new Command(async () => await Register());

        private bool Validate()
        {
            IsValidName = Name.Validate();
            IsValidOWA = OWA.Validate();
            IsValidStudentID = StudentID.Validate();
            IsValidPassword = Password.Validate();
            IsValidLibraryNumber = LibraryNumber.Validate();
            bool result = IsValidName && IsValidOWA && IsValidStudentID && IsValidPassword && IsValidLibraryNumber;

            return result;
        }
        private async Task Register()
        {
            IsBusy = true;

            bool isValid = Validate();
            //TODO: call Registration service and go to registration success page
            await Task.Delay(500);
            IsBusy = false;
        }
        public override Task InitializeAsync(object navigationData)
        {
            return base.InitializeAsync(navigationData);
        }

        private void AddValidationRules()
        {
            Name.Validations.Add(new NotEmptyRule<string>() {ValidationMessage = "Name cannot be empty"});
            OWA.Validations.Add(new NotEmptyRule<string>(){ValidationMessage = "OWA Username cannot be empty"});
            OWA.Validations.Add(new AlphaNumericRule<string>(){ValidationMessage = "A valid OWA Username is required"});
            Password.Validations.Add(new NotEmptyRule<string>() { ValidationMessage = "Password cannot be empty" });
            StudentID.Validations.Add(new DigitsRule<string>(){ValidationMessage = "A valid student ID is required"});
            StudentID.Validations.Add(new NotEmptyRule<string>(){ValidationMessage = "Student ID cannot be empty"});
            LibraryNumber.Validations.Add(new DigitsRule<string>() { ValidationMessage = "A valid library number is required" });
            LibraryNumber.Validations.Add(new NotEmptyRule<string>() { ValidationMessage = "Library number cannot be empty" });

        }
    }
}
