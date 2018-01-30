using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NottCS.Validations;
using Xamarin.Forms;

namespace NottCS.ViewModels
{
    internal class ForgotPasswordViewModel : BaseViewModel
    {
        public ValidatableObject<string> ValidatableObject { get; set; } = new ValidatableObject<string>();

        #region PublicMethodsWithPrivateBackingFields
        private string _owaAccount;

        private bool _isOwaValid;
        
        public string OWAAccount
        {
            get => _owaAccount;
            set => SetProperty(ref _owaAccount, value);
        }

        public bool IsOwaValid
        {
            get => _isOwaValid;
            set => SetProperty(ref _isOwaValid, value);
        }
        #endregion

        public ForgotPasswordViewModel()
        {
            Title = "Forgot Password";
            ValidatableObject.Validations.Add(new StringNotEmptyRule() { ValidationMessage = "No valid OWA detected." });
        }

        private async Task ForgotPassword()
        {
            Debug.WriteLine("Forgot Password Button Pressed.");
            IsBusy = true;
            IsOwaValid = ValidatableObject.Validate();

            if (IsOwaValid)
            {
                await Task.Delay(500);
                //TODO: Check acc availability, and communicate to email service on sending email.
                //TODO: Add popup dialog to notify user to check email
            }

            IsBusy = false;
        }

        public ICommand ForgotPasswordCommand => new Command(async () => await ForgotPassword());

        public override Task InitializeAsync(object navigationData)
        {
            if (navigationData is string s)
            {
                ValidatableObject.Value = s;
            }
            return base.InitializeAsync(navigationData);
        }
    }
}
