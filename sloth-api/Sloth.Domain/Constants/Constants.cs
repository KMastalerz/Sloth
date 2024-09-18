namespace Sloth.Domain.Constants;
public static class Endpoints
{
    public const string UIElements = "UIElements";
}

public static class DefualtConfigSystemOptions
{
    public const string PasswordComplexity = "{\"RequiredLength\":10,\"RequiredUniqueChars\":2,\"RequireNonAlphanumeric\":false,\"RequireLowercase\":true,\"RequireUppercase\":true,\"RequireDigit\":false}";
    public const int RefreshTokenLifetime = 3600;
    public const int TokenLifetime = 120;
}

public static class ConfigSystemOptions
{
    /// <summary>
    /// PasswordComplexity contains serialized PasswordOptions from Microsoft.AspNetCore.Identity;
    /// </summary>
    public const string PasswordComplexity = "PasswordComplexity";
    public const string RefreshTokenLifetime = "RefreshTokenLifetime";
    public const string TokenLifetime = "TokenLifetime";
}

public static class SlothClaims
{
    public const string UserGroup = "UserGroup";
}

