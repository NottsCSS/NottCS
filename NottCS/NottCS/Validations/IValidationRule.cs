using Newtonsoft.Json;
using NottCS.Services.JSONConverters;

namespace NottCS.Validations
{
    [JsonObject(ItemTypeNameHandling = TypeNameHandling.All)]
    public interface IValidationRule<T>
    {
        string ValidationMessage { get; }
        bool Check(T value);
    }
}
