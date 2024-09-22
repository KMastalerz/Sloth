namespace Sloth.Domain.DTO;
public class DefaultConfig
{
    public string PasswordOptions { get; set; } = default!;
    public int RefreshTokenLifetime { get; set; }
    public string TokenIssuer { get; set; } = default!;
    public string TokenKey { get; set; } = default!;
    public int TokenLifetime { get; set; }
}
