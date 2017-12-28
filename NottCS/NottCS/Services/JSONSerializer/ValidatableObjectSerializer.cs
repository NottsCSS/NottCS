using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NottCS.Services.JSONSerializer
{
    class ValidatableObjectSerializer : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
        public override bool CanRead => false;
    }
}
