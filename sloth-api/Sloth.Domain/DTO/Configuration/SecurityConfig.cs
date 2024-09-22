namespace Sloth.Domain.DTO;
public class SecurityConfig(DefaultConfig defaultConfig)
{
    public int TokenLifetime { get; set; } = defaultConfig.TokenLifetime;
    public int RefreshTokenLifetime { get; set; } = defaultConfig.RefreshTokenLifetime;
    public string TokenIssuer { get; set; } = defaultConfig.TokenIssuer;
    public string PasswordOptions { get; set; } = defaultConfig.PasswordOptions;
    public string TokenKey { get; set; } = defaultConfig.TokenKey;
}
