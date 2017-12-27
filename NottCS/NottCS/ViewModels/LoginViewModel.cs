﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

using NottCS.Validations;
using NottCS.Views;
using NottCS.Services.Navigation;

namespace NottCS.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        #region PublicPropertiesWithPrivateBackingFields
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
        #region AutomaticPublicProperties
        public ValidatableObject<string> UserName { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Password { get; set; } = new ValidatableObject<string>();
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
        #region PrivateMethods
        /// <summary>
        /// Adds validation rules to all user input fields
        /// </summary>
        private void AddValidationRules()
        {
            UserName.Validations.Add(new NotEmptyRule<string>() { ValidationMessage = "Username cannot be empty" });
            UserName.Validations.Add(new AlphaNumericRule<string>() { ValidationMessage = "Only OWA is accepted" });
            Password.Validations.Add(new NotEmptyRule<string>() { ValidationMessage = "Password cannot be empty" });
        }

        /// <summary>
        /// Validates all the user input fields to make sure everything is valid
        /// Errors are auto generated and displayed during validation process
        /// </summary>
        /// <returns>true all fields are valid, false otherwise</returns>
        private bool Validate()
        {
            IsValidUser = UserName.Validate();
            IsValidPassword = Password.Validate();

            return IsValidUser && IsValidPassword;
        }
        #endregion
        #region PrivateAsyncTasks
        /// <summary>
        /// Function that runs when sign in is called
        /// Checks for input field validity and call login service
        /// Navigates if both passes
        /// </summary>
        /// <returns></returns>
        private async Task SignInAsync()
        {
            IsBusy = true;

            bool isValid = Validate();
            //TODO: Implement login service under services, and then call the service from here

            //Delay to simulate real code running
            await Task.Delay(500);
            //Debugging code
            Debug.WriteLine("Sign in function called");

            IsBusy = false;
        }
        /// <summary>
        /// Function that runs when register is pressed
        /// Checks for input field validity and navigate
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
                await NavigationService.NavigateToAsync<RegistrationViewModel>(UserName.Value);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
            RegisterTextColor = Color.Black;
            IsBusy = false;
        }
        /// <summary>
        /// Function that runs when forgot password is pressed
        /// Navigates to forgot password page
        /// </summary>
        /// <returns></returns>
        private async Task ForgotPassword()
        {
            IsBusy = true;
            ForgotPasswordTextColor = Color.DarkBlue;

            //TODO: navigate to forgot password page
            //Delay to simulate real code running
            await Task.Delay(1500);

            //Debugging Code
            Debug.WriteLine("Forgot password function called");

            ForgotPasswordTextColor = Color.Black;
            IsBusy = false;
        }
        #endregion
    }
}