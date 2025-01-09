namespace sloth.Application.Models.Configuration;
public class Configuration
{
    public int InitialJobStatus { get; set; } = 1;
    public int MaxFailedLoginAttempts { get; set; } = 0;
    public int FailedLoginTimeout { get; set; } = 0;
    public List<string> AllowedHosts { get; set; } = [];
    public int PasswordLockTimeout { get; set; } = 0;
    public PasswordComplexity PasswordComplexity { get; set; } = new();
    public TokenConfiguration TokenConfiguration { get; set; } = new();
}
