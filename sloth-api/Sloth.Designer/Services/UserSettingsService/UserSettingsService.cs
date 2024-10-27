using Newtonsoft.Json;
using Sloth.Designer.Models;
using Sloth.Shared.Helpers;
using System.IO;
using System.Windows;

namespace Sloth.Designer.Services;

public class UserSettingsService : IUserSettingsService
{
    private readonly string userSettingJsonPath = string.Empty;
    private readonly UserSettingsItem userSettings = new();

    public UserSettingsService()
    {
        userSettingJsonPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "userSettings.json");

        if (File.Exists(userSettingJsonPath))
        {
            var json = File.ReadAllText(userSettingJsonPath);
            userSettings = JsonHelper.TryConvert(json, userSettings) ?? new();
        }
        else
        {
            MessageBox.Show("User settings file not found. Creating new settings file.");
            var json = JsonConvert.SerializeObject(userSettings);
            File.WriteAllText(userSettingJsonPath, json);
        }
    }
    private AccessTokenItem? GetDecryptedAccessToken()
    {
        var token = userSettings.Token;

        if (token is null) return null;

        return EncryptionHelper.DecryptObject<AccessTokenItem>(token);
    }

    public void SaveSettings(UserSettingsItem settings)
    {
        userSettings.RememberMe = settings.RememberMe;
        userSettings.SeedPath = settings.SeedPath;
        userSettings.Token = settings.Token;

        var json = JsonConvert.SerializeObject(userSettings);

        File.WriteAllText(userSettingJsonPath, json);
    }

    public void SaveToken(AccessTokenItem token)
    {
        var tokenString = EncryptionHelper.EncryptObject(token);
        userSettings.Token = tokenString;

        if (userSettings.RememberMe)
        {
            var json = JsonConvert.SerializeObject(userSettings);
            File.WriteAllText(userSettingJsonPath, json);
        }
    }

    public void SaveRememberMe(bool rememberMe)
    {
        userSettings.RememberMe = rememberMe;
        var json = JsonConvert.SerializeObject(userSettings);
        File.WriteAllText(userSettingJsonPath, json);
    }

    public void SaveSeedPath(string seedPath)
    {
        userSettings.SeedPath = seedPath;
        var json = JsonConvert.SerializeObject(userSettings);
        File.WriteAllText(userSettingJsonPath, json);
    }

    public void ClearToken()
    {
        userSettings.Token = null;
        var json = JsonConvert.SerializeObject(userSettings);
        File.WriteAllText(userSettingJsonPath, json);
    }

    public UserSettingsItem GetSettings()
    {
        return userSettings;
    }

    public string? GetToken()
    {
        var token = GetDecryptedAccessToken();
        return token?.AccessToken ?? null;
    }

    public string? GetRefreshToken()
    {
        var token = GetDecryptedAccessToken();
        return token?.RefreshToken ?? null;
    }

    public string? GetSeedPath()
    {
        return userSettings.SeedPath;
    }

    public bool GetRememberMe()
    {
        return userSettings.RememberMe;
    }

    public string? GetUserName()
    {
        var token = GetDecryptedAccessToken();

        if (token is null) return null;

        return token.User.UserName;
    }

    public bool IsLoogedIn()
    {
        return userSettings.Token is not null;
    }
}
