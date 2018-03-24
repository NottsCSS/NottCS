using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("NottCSTest")]
namespace NottCS.Validations
{
    internal class CharacterNumberRule : IValidationRule<string>
    {
        private int _numberOfCharacters;

        public CharacterNumberRule(int numberOfCharacters)
        {
            NumberOfCharacters = numberOfCharacters;
        }

        public string ValidationMessage { get; private set; }

        public int NumberOfCharacters
        {
            get => _numberOfCharacters;
            set
            {
                _numberOfCharacters = value;
                ValidationMessage = "cannot be less than " + value.ToString() + " characters";
            }
        }

        public bool Check(string value)
        {
            return value.Length <= NumberOfCharacters;
        }

        protected bool Equals(CharacterNumberRule other)
        {
            return string.Equals(ValidationMessage, other.ValidationMessage) && NumberOfCharacters == other.NumberOfCharacters;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CharacterNumberRule) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((ValidationMessage != null ? ValidationMessage.GetHashCode() : 0) * 397) ^ NumberOfCharacters;
            }
        }
    }
}
