using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NottCS.Validations;
using System.Diagnostics;
using System.Linq;
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
        private readonly List<string> _courseList = new List<string>();
        private ObservableCollection<string> _suggestions = new ObservableCollection<string>();

        #region PublicMethodsWithPrivateBackingFields
        private bool _isValidStudentID = true;
        private bool _isValidLibraryNumber = true;

        private User _currentUser = new User();
        private bool _isValidCourse = true;
        private string _selectedYearOfStudy;

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
                new StringNotEmptyRule(),
                new StringNumericRule()
            }
        };
        public ValidatableObject<string> LibraryNumber { get; set; } = new ValidatableObject<string>()
        {
            Validations =
            {
                new StringNotEmptyRule(),
                new StringNumericRule() 
            }
        };
        public ValidatableObject<string> Course { get; set; } = new ValidatableObject<string>()
        {
           Validations = { new StringNotEmptyRule(), new StringAlphaNumericRule()}
        };

        public ObservableCollection<string> Suggestions
        {
            get => _suggestions;
            set => SetProperty(ref _suggestions, value);
        }

        public ObservableCollection<string> YearOfStudy { get; set; } = new ObservableCollection<string>
        {
            "1",
            "2",
            "3",
            "4",
            "Other"
        };

        public string SelectedYearOfStudy
        {
            get => _selectedYearOfStudy;
            set => SetProperty(ref _selectedYearOfStudy, value);
        }

        public RegistrationViewModel()
        {
            Title = "NottCS Registration";
            AddCoursesToList();
        }
        public ICommand RegisterCommand => new Command(async () => await Register());
        public ICommand CourseItemSelectedCommand => new Command(CourseItemSelected);
        public ICommand CourseTextChangedCommand => new Command(CourseTextChanged);

        private bool Validate()
        {
            _isValidStudentID = StudentID.Validate();
            if (!_isValidStudentID)
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert($"Student ID {StudentID.Errors.First()}");
            }
            _isValidLibraryNumber = LibraryNumber.Validate();
            if (!_isValidLibraryNumber)
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert($"Library Number {LibraryNumber.Errors.First()}");
            }
            _isValidCourse = Course.Validate();
            if (!_isValidCourse)
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert($"Course {Course.Errors.First()}");
            }
            bool result = _isValidStudentID  && _isValidLibraryNumber && _isValidCourse;

            DebugService.WriteLine($"{_isValidStudentID} {_isValidLibraryNumber} {_isValidCourse}");
            return result;
        }

        private void AddCoursesToList()
        {
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
            if (isValid)
            {
                CurrentUser.StudentId = StudentID.Value;
                CurrentUser.LibraryNumber = LibraryNumber.Value;
                CurrentUser.Course = Course.Value;
                CurrentUser.YearOfStudy = SelectedYearOfStudy;

                var requestUpdate = await RestService.RequestUpdateAsync(CurrentUser);
                if (requestUpdate == "OK")
                {
                    var userData = await RestService.RequestGetAsync<User>("");
                    DebugService.WriteLine($"Respond from server is : {userData.Item1}");
                    if (userData.Item1 == "OK")
                    {
                        await NavigationService.NavigateToAsync<HomeViewModel>(userData.Item2);
                    }
                    else
                    {
                        Acr.UserDialogs.UserDialogs.Instance.Alert(requestUpdate, "Server Error", "OK");
                    }
                }
                else
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert(requestUpdate, "Server Error", "OK");
                }
            }
            IsBusy = false;
        }
        private void CourseItemSelected(object param)
        {
            if (!(param is string s)) return;
            DebugService.WriteLine($"{s} is selected");
            Course.Value = s;
        }

        private void CourseTextChanged(object courseEntryParameter)
        {
            if (!(courseEntryParameter is TextChangedEventArgs args)) return;
            var courseEntryString = args.NewTextValue;
            Suggestions.Clear();
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
            if (navigationData is User user)
            {
                DebugService.WriteLine("RegistrationViewModel receives User object");
                CurrentUser = user;
                StudentID.Value = user.StudentId;
                LibraryNumber.Value = user.LibraryNumber;
                Course.Value = user.Course;
                SelectedYearOfStudy = user.YearOfStudy;
            }

            return base.InitializeAsync(navigationData);
        }


    }
}
