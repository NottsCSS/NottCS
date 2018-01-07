using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NottCS.Validations;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using NottCS.Models;
using NottCS.Services.Navigation;
using Xamarin.Forms;

namespace NottCS.ViewModels
{
    internal class RegistrationViewModel : BaseViewModel
    {
        public RegistrationModel RegistrationParameters { get; set; } = new RegistrationModel();
        private readonly List<string> _courseList = new List<string>();
        #region PublicMethodsWithPrivateBackingFields
        private bool _isValidName = true;
        private bool _isValidOWA = true;
        private bool _isValidStudentID = true;
        private bool _isValidPassword = true;
        private bool _isValidLibraryNumber = true;
        private bool _isValidCourse = true;

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
            get => _isValidStudentID;
            set => SetProperty(ref _isValidStudentID, value);
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
        public bool IsValidCourse
        {
            get => _isValidCourse;
            set => SetProperty(ref _isValidLibraryNumber, value);
        }
        #endregion
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
            IsValidName = RegistrationParameters.Name.Validate();
            IsValidOWA = RegistrationParameters.OWA.Validate();
            IsValidStudentID = RegistrationParameters.StudentID.Validate();
            IsValidPassword = RegistrationParameters.Password.Validate();
            IsValidLibraryNumber = RegistrationParameters.LibraryNumber.Validate();
            IsValidCourse = RegistrationParameters.Course.Validate();
            bool result = IsValidName && IsValidOWA && IsValidStudentID && IsValidPassword && IsValidLibraryNumber;

            return result;
        }
        private void AddValidationRules()
        {
            RegistrationParameters.Name.Validations.Add(new NotEmptyRule<string>() { ValidationMessage = "Name cannot be empty" });
            RegistrationParameters.OWA.Validations.Add(new NotEmptyRule<string>() { ValidationMessage = "OWA Username cannot be empty" });
            RegistrationParameters.OWA.Validations.Add(new AlphaNumericRule<string>() { ValidationMessage = "A valid OWA Username is required" });
            RegistrationParameters.Password.Validations.Add(new NotEmptyRule<string>() { ValidationMessage = "Password cannot be empty" });
            RegistrationParameters.StudentID.Validations.Add(new DigitsRule<string>() { ValidationMessage = "A valid student ID is required" });
            RegistrationParameters.StudentID.Validations.Add(new NotEmptyRule<string>() { ValidationMessage = "Student ID cannot be empty" });
            RegistrationParameters.LibraryNumber.Validations.Add(new DigitsRule<string>() { ValidationMessage = "A valid library number is required" });
            RegistrationParameters.LibraryNumber.Validations.Add(new NotEmptyRule<string>() { ValidationMessage = "Library number cannot be empty" });
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
                string json="";
                try
                {
                    json = JsonConvert.SerializeObject(RegistrationParameters);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                    Debug.WriteLine(e.TargetSite);
                    Debug.WriteLine(e.Message);
                }
                Debug.WriteLine(json);
                try
                {
                    await NavigationService.NavigateToAsync<RegistrationSuccessViewModel>();
                }
                catch(Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
            await Task.Delay(500);
            IsBusy = false;
        }
        private void ItemSelected(object param)
        {
            if (!(param is string s)) return;
            Debug.WriteLine($"{s} is selected");
            RegistrationParameters.Course.Value = s;
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
                RegistrationParameters.OWA.Value = s;
            }
            return base.InitializeAsync(navigationData);
        }


    }
}
