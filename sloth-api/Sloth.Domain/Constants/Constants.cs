namespace Sloth.Domain.Constants;

public static class AuthScheme
{
    public static string Bearer = "Bearer";
}

public static class ConfigurationKeys
{
    public const string ConnectionString = "Sloth";
    public const string Configuration = "Configuration";
    public const string SeederOptions = "SeederOptions";
    public const string AllowedHosts = "AllowedHosts";
}

public static class SlothClaimTypes
{
    public const string Group = "Group";
}

public static class WebPages
{
    public const string Main = "Main";
    public const string Auth = "Auth";
}

public static class WebSecurity
{
    public const string Default = "Default";

    public static readonly IEnumerable<string> PublicPages = [
        "Auth"
    ];
}

