namespace Sloth.Shared.DTO;
public class AccessTokenResponse
{
    public string TokenType { get; set; } = default!;
    public string AccessToken { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
    public DateTime ExpiresAt { get; set; }
    public DateTime RefreshExpiresAt { get; set; }
    public AccessUser User { get; set; } = default!;
}
