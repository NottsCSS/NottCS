using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using NottCS.Services.JSONSerializer;
using NottCS.Validations;

namespace NottCS.Models
{
    public class EventAdditionalParameter
    {
        public string Name { get; set; }
        public List<IValidationRule<string>> ValidationList { get; set; } = new List<IValidationRule<string>>();
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
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (ValidationList != null ? ValidationList.GetHashCode() : 0);
            }
        }
    }
}
