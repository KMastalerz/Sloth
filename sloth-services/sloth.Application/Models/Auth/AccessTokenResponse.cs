namespace sloth.Application.Models.Auth;
public class AccessTokenResponse
{
    public string TokenType { get; set; } = default!;
    public string AccessToken { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
    public DateTime ExpiresAt { get; set; }
    public DateTime RefreshExpiresAt { get; set; }
}
