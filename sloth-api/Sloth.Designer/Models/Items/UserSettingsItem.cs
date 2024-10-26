namespace Sloth.Designer.Models;

public class UserSettingsItem
{
    public bool RememberMe { get; set; }
    public string? SeedPath { get; set; } = null;
    public string? Token { get; set; } = null;
}
