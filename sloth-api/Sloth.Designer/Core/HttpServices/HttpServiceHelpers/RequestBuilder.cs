namespace Sloth.Designer.Core;

public class RequestBuilder
{


    public static string BuildRequest (string basePath, string request, Dictionary<string, object?>? parms)
    {

        if (parms == null || !parms.Any() || parms.All(p=> p.Value == null))
            return $"{basePath}{request}";

        var queryString = string.Join("&", parms
            .Where(param => param.Value != null) // Skip null values
            .Select(param => $"{Uri.EscapeDataString(param.Key)}={Uri.EscapeDataString(Convert.ToString(param.Value, System.Globalization.CultureInfo.InvariantCulture)!)}"));

        return $"{basePath}{request}?{queryString}";
    }
    public static string BuildRequest(string basePath, string request)
    {
        return $"{basePath}{request}";
    }
}
