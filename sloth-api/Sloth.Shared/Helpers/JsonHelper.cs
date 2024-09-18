using Newtonsoft.Json;

namespace Sloth.Shared.Helpers;
public static class JsonHelper
{
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
