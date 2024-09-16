namespace Sloth.Infrastructure.DTO;
public class AppConfig
{
    public int TokenLifetime { get; set; }
    public int RefreshTokenLifetime { get; set; }
    public string PasswordComplexity { get; set; } = string.Empty;
    public IEnumerable<string> ActiveEndpoints { get; set; } = [];
}
