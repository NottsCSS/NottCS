using System;
using System.Collections.Generic;
using System.Text;
using NottCS.Validations;
using Xamarin.Forms;

namespace NottCS.CustomControls
{
    internal class ValidatableEntry : Entry
    {
        public static readonly BindableProperty ValidationsProperty =
            BindableProperty.Create(nameof(Validations), typeof(List<IValidationRule<string>>), typeof(ValidatableEntry));

        public static readonly BindableProperty IsValidProperty =
            BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(ValidatableEntry), true);

        public List<IValidationRule<string>> Validations {
            get => (List<IValidationRule<string>>)GetValue(ValidationsProperty);
            set => SetValue(ValidationsProperty, value);
        }

        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            set => SetValue(IsValidProperty, value);
        }

        private void ValidateOnTextChanged(object sender, TextChangedEventArgs args)
        {
            bool isValid = true;

            foreach (var rule in Validations)
            {
                isValid = isValid && rule.Check(args.NewTextValue);
            }

            IsValid = isValid; //only set the main IsValid property at the end to avoid binding getting changed constantly

            BackgroundColor = !isValid ? Color.Pink : Color.Default;
        }

        public ValidatableEntry()
        {
            TextChanged += ValidateOnTextChanged;
            BackgroundColor = Color.Pink;
        }
    }
}
