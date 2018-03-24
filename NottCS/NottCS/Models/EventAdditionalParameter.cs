using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using NottCS.Services.JSONConverters;
using NottCS.Validations;

namespace NottCS.Models
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore, ItemTypeNameHandling = TypeNameHandling.All)]
    public class EventAdditionalParameter
    {
        private string _errorMessage;
        public string Name { get; set; }
        public List<IValidationRule<string>> ValidationList { get; set; } = new List<IValidationRule<string>>();
        public string Value { get; set; }
        [JsonIgnore]
        public bool IsValid { get; set; }

        [JsonIgnore]
        public string ErrorMessage
        {
            get
            {
                if (String.IsNullOrEmpty(_errorMessage) && !IsValid)
                {
                    _errorMessage =  Name + " cannot be empty";
                }
                return _errorMessage;
            }
            set => _errorMessage = value;
        }

        protected bool Equals(EventAdditionalParameter other)
        {
            return string.Equals(Name, other.Name) && Equals(ValidationList, other.ValidationList) && string.Equals(Value, other.Value) && IsValid == other.IsValid && string.Equals(ErrorMessage, other.ErrorMessage);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is EventAdditionalParameter model))
                return false;
            if (model.Name != Name)
                return false;
            if (model.ValidationList.Count != ValidationList.Count)
                return false;
            for (int i = 0; i < model.ValidationList.Count; i++)
            {
                if (model.ValidationList[i].GetType() != ValidationList[i].GetType())
                    return false;
            }

            return true;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ValidationList != null ? ValidationList.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Value != null ? Value.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ IsValid.GetHashCode();
                hashCode = (hashCode * 397) ^ (ErrorMessage != null ? ErrorMessage.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
