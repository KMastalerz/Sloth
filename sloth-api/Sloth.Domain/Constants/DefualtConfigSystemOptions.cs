namespace Sloth.Domain.Constants;
public static class DefualtConfigSystemOptions
{
    public const string PasswordComplexity = "{\"RequiredLength\":10,\"RequiredUniqueChars\":2,\"RequireNonAlphanumeric\":false,\"RequireLowercase\":true,\"RequireUppercase\":true,\"RequireDigit\":false}";
    public const int RefreshTokenLifetime = 3600;
    public const int TokenLifetime = 120;
}