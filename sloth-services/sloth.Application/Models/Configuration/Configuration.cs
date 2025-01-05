namespace sloth.Application.Models.Configuration;
public class Configuration
{
    public int MaxFailedLoginAttempts { get; set; }
    public int FailedLoginTimeout { get; set; }
    public List<string> AllowedHosts { get; set; } = [];
    public int PasswordLockTimeout { get; set; }
    public PasswordComplexity PasswordComplexity { get; set; } = new();
    public TokenConfiguration TokenConfiguration { get; set; } = new();
}
