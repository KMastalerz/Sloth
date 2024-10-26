namespace Sloth.Designer.Models;

public class AccessTokenItem
{
    public string TokenType { get; set; } = default!;
    public string AccessToken { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
    public DateTime ExpiresAt { get; set; }
    public DateTime RefreshExpiresAt { get; set; }
    public AccessUserItem User { get; set; } = default!;
}
