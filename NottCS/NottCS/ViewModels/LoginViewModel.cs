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
            UserName.Validations.Add(new NotEmptyRule<string>() { ValidationMessage = "Username cannot be empty" });
            UserName.Validations.Add(new AlphaNumericRule<string>() { ValidationMessage = "Only OWA is accepted" });
            Password.Validations.Add(new NotEmptyRule<string>() { ValidationMessage = "Password cannot be empty" });
        }

        private bool Validate()
        {
            IsValidUser = UserName.Validate();
            IsValidPassword = Password.Validate();

            return IsValidUser && IsValidPassword;
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
            
            try
            {
                const string asd = "Hello world";
                await NavigationService.NavigateToAsync<RegistrationViewModel>(asd);
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