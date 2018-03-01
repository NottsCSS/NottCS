using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NottCS.Validations;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using NottCS.Models;
using NottCS.Services;
using NottCS.Services.Navigation;
using NottCS.Services.REST;
using Xamarin.Forms;

namespace NottCS.ViewModels
{
    internal class RegistrationViewModel : BaseViewModel
    {
//        private readonly List<string> _courseList = new List<string>();
        #region PublicMethodsWithPrivateBackingFields
        private bool _isValidStudentID = true;
        private bool _isValidLibraryNumber = true;

        private User _currentUser = null;
//        private bool _isValidCourse = true;

//        private ObservableCollection<string> _suggestions = new ObservableCollection<string>();
        
        public bool IsValidStudentID
        {
            get => _isValidStudentID;
            set => SetProperty(ref _isValidStudentID, value);
        }
        public bool IsValidLibraryNumber
        {
            get => _isValidLibraryNumber;
            set => SetProperty(ref _isValidLibraryNumber, value);
        }

        //        public bool IsValidCourse
        //        {
        //            get => _isValidCourse;
        //            set => SetProperty(ref _isValidLibraryNumber, value);
        //        }
        #endregion

        public User CurrentUser
        {
            get => _currentUser;
            set => SetProperty(ref _currentUser, value);
        }

        public ValidatableObject<string> StudentID { get; set; } = new ValidatableObject<string>()
        {
            Validations =
            {
                new StringNotEmptyRule() {ValidationMessage = "Student ID cannot be empty"},
                new StringDigitsRule() {ValidationMessage = "A valid student ID is required"}
            }
        };
        public ValidatableObject<string> LibraryNumber { get; set; } = new ValidatableObject<string>()
        {
            Validations =
            {
                new StringNotEmptyRule() {ValidationMessage = "Library number cannot be empty"},
                new StringDigitsRule() {ValidationMessage = "A valid library number is required"}
            }
        };

        //        public ObservableCollection<string> Suggestions
        //        {
        //            get => _suggestions;
        //            set => SetProperty(ref _suggestions, value);
        //        }


        public RegistrationViewModel()
        {
            Title = "NottCS Registration";
        }
        public ICommand RegisterCommand => new Command(async () => await Register());
//        public ICommand ItemSelectedCommand => new Command(ItemSelected);
//        public ICommand TextChangedCommand => new Command(CourseTextChanged);

        private bool Validate()
        {
            IsValidStudentID = StudentID.Validate();
            IsValidLibraryNumber = LibraryNumber.Validate();
//            IsValidCourse = Course.Validate();
            bool result = IsValidStudentID  && IsValidLibraryNumber;

            return result;
        }

        //private void AddCoursesToList()
        //{
        //    //TODO: get courses from database instead of hardcoding
        //    _courseList.Add("Foundation in Engineering");
        //    _courseList.Add("Mechanical Engineering");
        //    _courseList.Add("Mechatronic Engineering");
        //    _courseList.Add("Electrical and Electronic Engineering");
        //    _courseList.Add("Chemical Engineering");
        //    _courseList.Add("Chemical Engineering with Environmental Engineering");
        //    _courseList.Add("Civil Engineering");
        //    _courseList.Add("Applied Mathematics");
        //    _courseList.Add("Computer Science");
        //    _courseList.Add("Computer Science with Artificial Intelligence");
        //    _courseList.Add("Computer Science and Management Studies");
        //    _courseList.Add("Software Engineering");
        //    _courseList.Add("Foundation in Computer Science");
        //}

        private async Task Register()
        {
            IsBusy = true;

            bool isValid = Validate();
            if (isValid)
            {
                CurrentUser.StudentId = StudentID.Value;
                CurrentUser.LibraryNumber = LibraryNumber.Value;
                Tuple<bool, User> userData = null;
                try
                {
                    userData = await RestService.RequestPutAsync(CurrentUser);
                }
                catch(Exception e)
                {
                    DebugService.WriteLine(e.Message);
                }

                if (userData == null)
                {
                    DebugService.WriteLine("Unable to get result from put request");
                    IsBusy = false;
                    return;
                }

                if (userData.Item1) //if request is successful
                {
//                    await NavigationService.NavigateToAsync<RegistrationSuccessViewModel>();
                    await NavigationService.NavigateToAsync<AccountViewModel>(userData.Item2);
                }
                else
                {
                    DebugService.WriteLine("Request unsuccessful. Please try again");
                }

            }
            IsBusy = false;
        }
//        private void ItemSelected(object param)
//        {
//            if (!(param is string s)) return;
//            DebugService.WriteLine($"{s} is selected");
//            RegistrationParameters.Course.Value = s;
//        }
//
//        private void CourseTextChanged(object courseEntryParameter)
//        {
//            Suggestions.Clear();
//            if (!(courseEntryParameter is string courseEntryString)) return;
//            foreach (string course in _courseList)
//            {
//                if (course.ToUpper().Contains(courseEntryString.ToUpper()) && Suggestions.Count < 5 && course!=courseEntryString)
//                {
//                    Suggestions.Add(course);
//                }
//            }
//        }
        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData is User user)
            {
                CurrentUser = user;
            }
            return base.InitializeAsync(navigationData);
        }


    }
}
