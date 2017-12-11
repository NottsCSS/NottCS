using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace NottCS.Behaviors
{
    public class AlphaNumericValidationBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            Regex r = new Regex("^[a-zA-Z0-9]*$");
            bool isValid = r.IsMatch(args.NewTextValue);
            ((Entry)sender).TextColor = isValid ? Color.Default : Color.Red;
        }
    }
}
