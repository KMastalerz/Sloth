namespace sloth.Application.Models.Configuration;
public class TokenConfiguration
{
    public string TokenIssuer { get; set; } = default!;
    public string TokenKey { get; set; } = default!;
    public int TokenLifetime { get; set; }
    public string RefreshTokenKey { get; set; } = default!;
    public int RefreshTokenLifetime { get; set; }
}