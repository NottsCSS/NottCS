using Newtonsoft.Json;
using NottCS.Services.JSONSerializer;

namespace NottCS.Validations
{
    [JsonConverter(typeof(IValidationRuleConverter))]
    public interface IValidationRule<T>
    {
        string ValidationMessage { get; }
        bool Check(T value);
    }
}
