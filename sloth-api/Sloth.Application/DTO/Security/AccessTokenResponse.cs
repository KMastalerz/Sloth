namespace Sloth.Application.DTO.Security;
public class AccessTokenResponse
{
    public string TokenType { get; set; } = default!;
    public string AccessToken { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
    public TimeSpan ExpiresIn { get; set; }
    public AccessUser User { get; set; } = default!;
}
