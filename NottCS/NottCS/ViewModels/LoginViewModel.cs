using System;
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
        #region PrivateBackingFields
        private ValidatableObject<string> _userName = new ValidatableObject<string>();
        private ValidatableObject<string> _password = new ValidatableObject<string>();
        private Color _registerTextColor = Color.Black;
        private Color _forgotPasswordTextColor = Color.Black;
        #endregion
        #region PublicPropertiesWithPrivateBackingFields
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
        #endregion
        //Automatic public properties
        public LoginViewModel()
        {
            Title = "NottCS Login";
            AddValidationRules();
        }

        public override Task InitializeAsync(object navigationData)
        {
            return base.InitializeAsync(navigationData);
        }

        public ICommand SignInCommand => new Command(async () => await SignInAsync());
        public ICommand RegisterCommand => new Command(async () => await Register());
        public ICommand ForgotPasswordCommand => new Command(async () => await ForgotPassword());

        #region PrivateMethods
        private void AddValidationRules()
        {
            _userName.Validations.Add(new NotEmptyRule<string>() { ValidationMessage = "Username cannot be empty" });
            _userName.Validations.Add(new AlphaNumericRule<string>() { ValidationMessage = "Only OWA is accepted" });
            _password.Validations.Add(new NotEmptyRule<string>() { ValidationMessage = "Password cannot be empty" });
        }

        private bool Validate()
        {
            bool isValidUser = _userName.Validate();
            bool isValidPassword = _password.Validate();

            return isValidUser && isValidPassword;
        }
        #endregion
        #region PrivateAsyncTasks
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
            RegisterTextColor = Color.DarkBlue;
            //Debugging code
            Debug.WriteLine("Registration function called");


            //TODO: implement navigation to registration page
            //Delay to simulate real code running
            try
            {
                await NavigationService.NavigateToAsync<RegistrationViewModel>();
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
            }
            RegisterTextColor = Color.Black;
            IsBusy = false;
        }
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