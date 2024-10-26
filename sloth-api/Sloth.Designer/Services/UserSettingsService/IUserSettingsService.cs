using Sloth.Designer.Models;

namespace Sloth.Designer.Services;
public interface IUserSettingsService
{
    void SaveRememberMe(bool rememberMe);
    void SaveSeedPath(string seedPath);
    void SaveSettings(UserSettingsItem settings);
    void SaveToken(AccessTokenItem token);
    void ClearToken();
    bool GetRememberMe();
    string? GetSeedPath();
    UserSettingsItem GetSettings();
    string? GetToken();
    string? GetRefreshToken();
    string? GetUserName();
    bool IsLoogedIn();
}