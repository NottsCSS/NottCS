using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

using NottCS.Validations;
using NottCS.Views;

namespace NottCS.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private ValidatableObject<string> _userName = new ValidatableObject<string>();
        private ValidatableObject<string> _password = new ValidatableObject<string>();
        private Color _registerTextColor = Color.Black;
        private Color _forgotPasswordTextColor = Color.Black;
        private bool _registerBold = false;
        private bool _forgotPasswordBold = false;

        //Public properties with private backing field
        public ValidatableObject<string> UserName
        {
            get => _userName;
            set => _userName = value;
        }
        public ValidatableObject<string> Password
        {
            get => _password;
            set => _password = value;
        }
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

        //Automatic public properties
        public bool RegisterBold
        {
            get => _registerBold;
            set => SetProperty(ref _registerBold, value);
        }
        public bool ForgotPasswordBold
        {
            get => _forgotPasswordBold;
            set => SetProperty(ref _forgotPasswordBold, value);
        }

        public LoginViewModel()
        {
            Title = "NottCS Login";
            AddValidationRules();
        }
        public ICommand SignInCommand => new Command(async () => await SignInAsync());
        public ICommand RegisterCommand => new Command(async () => await Register());
        public ICommand ForgotPasswordCommand => new Command(async () => await ForgotPassword());

        //Private methods
        private void AddValidationRules()
        {
            _userName.Validations.Add(new NotEmptyRule<string>() {ValidationMessage = "Username cannot be empty"});
            _userName.Validations.Add(new AlphaNumericRule<string>() { ValidationMessage = "Only OWA is accepted" });
            _password.Validations.Add(new NotEmptyRule<string>() { ValidationMessage = "Password cannot be empty" });
        }
        
        private bool Validate()
        {
            bool isValidUser = _userName.Validate();
            bool isValidPassword = _password.Validate();

            return isValidUser && isValidPassword;
        }
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

        private async Task Register()
        {
            IsBusy = true;
            RegisterBold = true;
            RegisterTextColor = Color.DarkBlue;

            //TODO: implement navigation to registration page
            //Delay to simulate real code running
            await Task.Delay(1500);
            //Debugging code
            Debug.WriteLine("Registration function called");

            RegisterTextColor = Color.Black;
            RegisterBold = false;
            IsBusy = false;
        }

        private async Task ForgotPassword()
        {
            IsBusy = true;
            ForgotPasswordTextColor = Color.DarkBlue;
            ForgotPasswordBold = true;

            //TODO: navigate to forgot password page
            //Delay to simulate real code running
            await Task.Delay(1500);

            //Debugging Code
            Debug.WriteLine("Forgot password function called");

            ForgotPasswordTextColor = Color.Black;
            ForgotPasswordBold = false;
            IsBusy = false;
        }
    }
}