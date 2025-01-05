namespace sloth.API.Extensions;

public static class IntExtensions
{
    public static string GetStatusTypeLink(this int value)
    {
        var baseAddress = "https://tools.ietf.org/html/rfc9110";
        return value switch
        {
            400 => $"{baseAddress}#section-15.5.1", // Bad Request
            401 => $"{baseAddress}#section-15.5.2", // Unauthorized
            403 => $"{baseAddress}#section-15.5.4", // Forbidden
            404 => $"{baseAddress}#section-15.5.5", // Not Found
            409 => $"{baseAddress}#section-15.5.10", // Conflict
            423 => $"{baseAddress}#section-11.3",   // Locked (WebDAV)
            500 => $"{baseAddress}#section-15.6.1", // Internal Server Error
            _ => $"{baseAddress}" // Default to main RFC 9110 document
        };
    }
}
