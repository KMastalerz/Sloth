using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Sloth.Shared.Helpers;
public static class JsonHelper
{
    private static readonly JsonSerializerSettings camelCaseSettings = new JsonSerializerSettings
    {
        ContractResolver = new CamelCasePropertyNamesContractResolver(),
        NullValueHandling = NullValueHandling.Ignore 
    };

    public static string SerializeToCamelCase<T>(this T obj)
    {
        return JsonConvert.SerializeObject(obj, camelCaseSettings);
    }

    public static T? TryConvert<T>(this string jsonString, T? defaultObject) where T : class, new()
    {
        try
        {
            var result = JsonConvert.DeserializeObject<T>(jsonString);
            return result ?? defaultObject;
        }
        catch
        {
            return defaultObject;
        }
    }
}
