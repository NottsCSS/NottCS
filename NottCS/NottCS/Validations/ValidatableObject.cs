using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using NottCS.Services.JSONSerializer;
using NottCS.ViewModels;

[assembly: InternalsVisibleTo("NottCSTest")]
namespace NottCS.Validations
{
    [JsonConverter(typeof(ValidatableObjectJsonConverter))]
    internal class ValidatableObject<T> : BaseViewModel
    {
        private readonly List<IValidationRule<T>> _validations;
        private List<string> _errors;
        private T _value;
        private bool _isValid;

        public List<IValidationRule<T>> Validations => _validations;

        public List<string> Errors
        {
            get => _errors;
            set => SetProperty(ref _errors, value);
        }

        public T Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        private bool IsValid
        {
            get => _isValid;
            set => SetProperty(ref _isValid, value);
        }

        public ValidatableObject()
        {
            _isValid = true;
            _errors = new List<string>();
            _validations = new List<IValidationRule<T>>();
        }

        public bool Validate()
        {
            Errors.Clear();

            IEnumerable<string> errors = _validations.Where(v => !v.Check(Value))
                .Select(v => v.ValidationMessage);

            Errors = errors.ToList();
            IsValid = !Errors.Any();

            return IsValid;
        }

        public override string ToString()
        {
            return Value != null ? Value.ToString() : "NULL";
        }
    }
}