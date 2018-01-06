using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NottCS.Validations;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using NottCS.Services.Navigation;
using Xamarin.Forms;

namespace NottCS.ViewModels
{
    class RegistrationViewModel : BaseViewModel
    {
        private List<string> _courseList = new List<string>();
        #region PublicMethodsWithPrivateBackingFields
        private bool _isValidName = true;
        private bool _isValidOWA = true;
        private bool _isValidStudentId = true;
        private bool _isValidPassword = true;
        private bool _isValidLibraryNumber = true;
        private string _course;
        private ObservableCollection<string> _suggestions = new ObservableCollection<string>();

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
        public string Course
        {
            get => _course;
            set => SetProperty(ref _course, value);
        }
        #endregion
        public ValidatableObject<string> Name { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> OWA { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> StudentID { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Password { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> LibraryNumber { get; set; } = new ValidatableObject<string>();

        public ObservableCollection<string> Suggestions
        {
            get => _suggestions;
            set => SetProperty(ref _suggestions, value);
        }


        public RegistrationViewModel()
        {
            Title = "NottCS Registration";
            AddValidationRules();
            AddCoursesToList();
        }
        public ICommand RegisterCommand => new Command(async () => await Register());
        public ICommand ItemSelectedCommand => new Command(ItemSelected);
        public ICommand TextChangedCommand => new Command(CourseTextChanged);

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

        private void AddCoursesToList()
        {
            //TODO: get courses from database instead of hardcoding
            _courseList.Add("Foundation in Engineering");
            _courseList.Add("Mechanical Engineering");
            _courseList.Add("Mechatronic Engineering");
            _courseList.Add("Electrical and Electronic Engineering");
            _courseList.Add("Chemical Engineering");
            _courseList.Add("Chemical Engineering with Environmental Engineering");
            _courseList.Add("Civil Engineering");
            _courseList.Add("Applied Mathematics");
            _courseList.Add("Computer Science");
            _courseList.Add("Computer Science with Artificial Intelligence");
            _courseList.Add("Computer Science and Management Studies");
            _courseList.Add("Software Engineering");
            _courseList.Add("Foundation in Computer Science");
        }

        private async Task Register()
        {
            IsBusy = true;

            bool isValid = Validate();
            //TODO: call Registration service and go to registration success page
            if (isValid)
            {
                await NavigationService.NavigateToAsync<RegistrationSuccessViewModel>();
            }
            await Task.Delay(500);
            IsBusy = false;
        }
        private void ItemSelected(object param)
        {
            if (!(param is string s)) return;
            Debug.WriteLine($"{s} is selected");
            Course = s;
        }

        private void CourseTextChanged(object courseEntryParameter)
        {
            Suggestions.Clear();
            if (!(courseEntryParameter is string courseEntryString)) return;
            foreach (string course in _courseList)
            {
                if (course.ToUpper().Contains(courseEntryString.ToUpper()) && Suggestions.Count < 5 && course!=courseEntryString)
                {
                    Suggestions.Add(course);
                }
            }
        }
        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData is string s)
            {
                OWA.Value = s;
            }
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
