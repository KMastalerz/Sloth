using Sloth.Designer.Core;
using Sloth.Designer.Models;

namespace Sloth.Designer.Services;

internal class AuthService(IHttpServices httpServices, IUserSettingsService userSettingsService) : IAuthService
{
    public async Task<bool> Login(string login, string password)
    {
        var response = await httpServices.PostAsync<AccessTokenItem>(HttpServicePaths.Auth, "Login", new
        {
            Login = login,
            Password = password
        });

        if (response.Success)
        {
            var token = response.Data;

            var rememberMe = userSettingsService.GetRememberMe();
            if(rememberMe)
                userSettingsService.SaveToken(token!);

            return true;
        }

        return false;
    }

    public async Task Refreshtoken(string username, string password)
    {
        var response = await httpServices.PostAsync<AccessTokenItem>(HttpServicePaths.Auth, "RefreshToken", new
        {
            UserName = username,
            Password = password
        });

        if (response.Success)
        {
            var token = response.Data;

            var rememberMe = userSettingsService.GetRememberMe();
            if (rememberMe)
                userSettingsService.SaveToken(token!);
        }
    }

    public void Logoff()
    {
        userSettingsService.ClearToken();
    }
}
