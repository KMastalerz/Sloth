namespace Sloth.Domain.Constants;

public static class AuthScheme
{
    public static string Bearer = "Bearer";
}
public static class Config
{
    /// <summary>
    /// PasswordComplexity contains serialized PasswordOptions from Microsoft.AspNetCore.Identity;
    /// </summary>
    public const string PasswordOptions = "PasswordOptions";
    public const string RefreshTokenLifetime = "RefreshTokenLifetime";
    public const string TokenIssuer = "TokenIssuer";
    public const string TokenKey = "TokenKey";
    public const string TokenLifetime = "TokenLifetime";


    public static readonly IEnumerable<string> ConfigOptions = [
        PasswordOptions,
        RefreshTokenLifetime,
        TokenIssuer,
        TokenKey,
        TokenLifetime
    ];
}

public static class ConfigurationKeys
{
    public const string ConnectionString = "Sloth";
    public const string DefaultConfiguration = "DefaultConfiguration";
}


public static class Endpoints
{
    public const string UIElements = "UIElements";
}

public static class SlothClaimTypes
{
    public const string Group = "Group";
}


public static class WebPages
{
    public const string MainPage = "MainPage";
    public const string LoginPage = "LoginPage";
}

