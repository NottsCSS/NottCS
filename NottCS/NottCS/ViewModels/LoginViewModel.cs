using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using NottCS.Models;
using Xamarin.Forms;
using Newtonsoft.Json;

using NottCS.Validations;
using NottCS.Services.Navigation;

namespace NottCS.ViewModels
{
    internal class LoginViewModel : BaseViewModel
    {
        #region NonInputProperties
        //Private Backing Fields
        private Color _registerTextColor = Color.Black;
        private Color _forgotPasswordTextColor = Color.Black;

        private bool _isValidUser = true;
        private bool _isValidPassword = true;
        //Public Properties
        public Color RegisterTextColor
        {
            get => _registerTextColor;
            set => SetProperty(ref _registerTextColor,value);
        }
        public Color ForgotPasswordTextColor
        {
            get => _forgotPasswordTextColor;
            set => SetProperty(ref _forgotPasswordTextColor, value);
        }

        public bool IsValidUser
        {
            get => _isValidUser;
            set => SetProperty(ref _isValidUser, value);
        }
        public bool IsValidPassword
        {
            get => _isValidPassword;
            set => SetProperty(ref _isValidPassword, value);
        }

        #endregion
        #region InputProperties
        public LoginModel LoginParameters { get; set; } = new LoginModel();
        #endregion
        /// <summary>
        /// Constructor to initialise values of various fields
        /// </summary>
        public LoginViewModel()
        {
            Title = "NottCS Login";
            AddValidationRules();
        }
        /// <summary>
        /// Command wrapper for sign in function
        /// </summary>
        public ICommand SignInCommand => new Command(async () => await SignInAsync());
        /// <summary>
        /// Command wrapper for register function
        /// </summary>
        public ICommand RegisterCommand => new Command(async () => await Register());
        /// <summary>
        /// Command wrapper for forgot password function
        /// </summary>
        public ICommand ForgotPasswordCommand => new Command(async () => await ForgotPassword());
        /// <summary>
        /// Adds validation rules to all user input fields
        /// </summary>
        private void AddValidationRules()
        {
            LoginParameters.Username.Validations.Add(new StringNotEmptyRule() { ValidationMessage = "Username cannot be empty" });
            LoginParameters.Username.Validations.Add(new StringAlphaNumericRule() { ValidationMessage = "Only OWA is accepted" });
            LoginParameters.Password.Validations.Add(new StringNotEmptyRule() { ValidationMessage = "Password cannot be empty" });
        }
        /// <summary>
        /// <para>Validates all the user input fields to make sure everything is valid</para>
        /// <para>Errors are auto generated and displayed during validation process</para>
        /// </summary>
        /// <returns>true all fields are valid, false otherwise</returns>
        private bool Validate()
        {
            IsValidUser = LoginParameters.Username.Validate();
            IsValidPassword = LoginParameters.Password.Validate();

            return IsValidUser && IsValidPassword;
        }
        /// <summary>
        /// <para>Function that runs when sign in is called</para>
        /// <para>Checks for input field validity and call login service</para>
        /// <para>Navigates if both passes</para>
        /// </summary>
        /// <returns></returns>
        private async Task SignInAsync()
        {
            IsBusy = true;

            bool isValid = Validate();
            //TODO: Implement login service under services, and then call the service from here

            
            if (isValid)
            {
                string json = "";
                try
                {
                    json = JsonConvert.SerializeObject(LoginParameters);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.TargetSite);
                    Debug.WriteLine(e.Message);
                }
                Debug.WriteLine(json);
            }
            //Delay to simulate real code running
            await Task.Delay(500);
            //Debugging code
            Debug.WriteLine("Sign in function called");

            IsBusy = false;
        }
        /// <summary>
        /// <para>Function that runs when register is pressed</para>
        /// <para>Checks for input field validity and navigate</para>
        /// </summary>
        /// <returns></returns>
        private async Task Register()
        {
            IsBusy = true;
            RegisterTextColor = Color.DarkBlue;
            //Debugging code
            Debug.WriteLine("Registration function called");
            try
            {
                await NavigationService.NavigateToAsync<RegistrationViewModel>(LoginParameters.Username.Value);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
            RegisterTextColor = Color.Black;
            IsBusy = false;
        }
        /// <summary>
        /// <para>Function that runs when forgot password is pressed</para>
        /// <para>Navigates to forgot password page</para>
        /// </summary>
        /// <returns></returns>
        private async Task ForgotPassword()
        {
            IsBusy = true;
            ForgotPasswordTextColor = Color.DarkBlue;

            //TODO: navigate to forgot password page
            //Delay to simulate real code running
            //await Task.Delay(1500);

            //Debugging Code
            Debug.WriteLine("Forgot password function called");
            try
            {
                await NavigationService.NavigateToAsync<ForgotPasswordViewModel>(LoginParameters.Username.Value);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }

            ForgotPasswordTextColor = Color.Black;
            IsBusy = false;
        }
    }
}