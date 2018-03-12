using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NottCS.Validations;

[assembly: InternalsVisibleTo("NottCSTest")]
namespace NottCS.Services.JSONSerializer
{
    internal class IValidationRuleConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.GetType().ToString());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string typeString = (string)reader.Value;
            string assembly = typeof(IValidationRule<string>).Assembly.FullName;
            string assemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", typeString, assembly);
            Type validationType = Type.GetType(assemblyName);


            return Activator.CreateInstance(validationType);
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
        public override bool CanRead => true;
    }
}
