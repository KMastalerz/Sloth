namespace Sloth.Domain.Constants;
public static class ConfigSystemOptions
{
    /// <summary>
    /// PasswordComplexity contains serialized PasswordOptions from Microsoft.AspNetCore.Identity;
    /// </summary>
    public const string PasswordComplexity = "PasswordComplexity";
    public const string RefreshTokenLifetime = "RefreshTokenLifetime";
    public const string TokenLifetime = "TokenLifetime";
}

