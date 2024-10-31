using Sloth.Designer.Constants;
using Sloth.Designer.Helpers;
using Sloth.Designer.Models;
using System.Net.Http;

namespace Sloth.Designer.Services;

internal class AuthService : IAuthService
{
    private readonly HttpClient httpClient;
    public AuthService(IHttpClientFactory httpClientFactory)
    {
        httpClient = httpClientFactory.CreateClient("ApiBaseClient");
    }

    public async Task<ServiceReturnValue<AccessTokenItem>?> Login(string login, string password)
    {

        return await httpClient.PostAsync<AccessTokenItem>(HttpServicePaths.Auth, "Login", new
        {
            Login = login,
            Password = password
        });
    }

    public async Task<ServiceReturnValue<AccessTokenItem>?> Refreshtoken(string username, string refreshToken)
    {

        return await httpClient.PostAsync<AccessTokenItem>(HttpServicePaths.Auth, "RefreshToken", new
        {
            UserName = username,
            RefreshToken = refreshToken
        });
    }
}
