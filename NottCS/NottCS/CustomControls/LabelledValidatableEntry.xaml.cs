using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NottCS.Validations;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NottCS.CustomControls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LabelledValidatableEntry : ContentView
	{

	    public static readonly BindableProperty NameProperty =
	        BindableProperty.Create(nameof(Name), typeof(string), typeof(LabelledValidatableEntry));

	    public static readonly BindableProperty ValueProperty =
	        BindableProperty.Create(nameof(Value), typeof(string), typeof(LabelledValidatableEntry));

        public static readonly BindableProperty ValidationsProperty =
	        BindableProperty.Create(nameof(Validations), typeof(List<IValidationRule<string>>), typeof(LabelledValidatableEntry));

        public static readonly BindableProperty ErrorMessageProperty =
	        BindableProperty.Create(nameof(ErrorMessage), typeof(string), typeof(LabelledValidatableEntry));

	    public static readonly BindableProperty IsValidProperty =
	        BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(LabelledValidatableEntry), true);

        public string Name
        {
	        get => (string)GetValue(NameProperty);
	        set => SetValue(NameProperty, value);
	    }
	    public string Value
	    {
	        get => (string)GetValue(ValueProperty);
	        set => SetValue(ValueProperty, value);
	    }
        public List<IValidationRule<string>> Validations
	    {
	        get => (List<IValidationRule<string>>)GetValue(ValidationsProperty);
	        set => SetValue(ValidationsProperty, value);
	    }
        public string ErrorMessage
	    {
	        get => (string)GetValue(ErrorMessageProperty);
	        private set => SetValue(ErrorMessageProperty, value);
	    }
	    public bool IsValid
	    {
	        get => (bool)GetValue(IsValidProperty);
	        private set => SetValue(IsValidProperty, value);
	    }

	    private void ValidateOnTextChanged(object sender, TextChangedEventArgs args)
	    {
	        bool isValid = true;
	        bool isFirst = true;

	        foreach (var validationRule in Validations)
	        {
	            bool valid = validationRule.Check(args.NewTextValue);

                if (!valid && isFirst)
                {
                    ErrorMessage = Name + ' ' + validationRule.ValidationMessage;
                    isValid = false;
                    isFirst = false;
                }
	        }

	        IsValid = isValid; //only set the main IsValid property at the end to avoid binding getting changed constantly

	        EntryObject.BackgroundColor = !isValid ? Color.Pink : Color.Default;
	    }


        public LabelledValidatableEntry ()
		{
			InitializeComponent ();
            EntryObject.BackgroundColor = Color.Pink;
		}
	}
}