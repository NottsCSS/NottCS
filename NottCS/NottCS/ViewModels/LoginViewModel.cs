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
        public LoginViewModel()
        {
            Title = "NottCS Login";
            _userName.Validations.Add(new AlphaNumericValidation<string>(){ValidationMessage = "A valid username is required"});
        }



        public ValidatableObject<string> UserName
        {
            get => _userName;
            set => _userName = value;
        }
        public ICommand OpenWebCommand { get; }
        public ICommand SignInCommand => new Command(async () => await SignInAsync());
        private bool Validate()
        {
            bool isValidUser = _userName.Validate();

            return isValidUser;
        }
        private async Task SignInAsync()
        {
            IsBusy = true;

            bool isValid = Validate();
            await Task.Delay(500);
            Debug.WriteLine("Sign in attempted");
            foreach (var error in _userName.Errors)
            {
                Debug.WriteLine(error);
            }
            IsBusy = false;
        }
    }
}