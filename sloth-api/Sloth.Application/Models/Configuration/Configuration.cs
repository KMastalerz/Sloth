namespace Sloth.Application.Models;
public class Configuration
{
    public PasswordComplexity PasswordComplexity { get; set; } = default!;
    public string TokenIssuer { get; set; } = default!;
    public string TokenKey { get; set; } = default!;
    public int TokenLifetime { get; set; } = default!;
    public string RefreshTokenKey { get; set; } = default!;
    public int RefreshTokenLifetime { get; set; } = default!;
}
