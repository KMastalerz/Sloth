using Sloth.Designer.Pages;
using Sloth.Designer.Services;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Sloth.Designer.Core;
public class AuthenticatedHttpClientHandler(IUserSettingsService userSettingsService, IAuthService authService, IWindowService windowService) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Get the bearer token
        var bearerToken = userSettingsService.GetToken();

        if (bearerToken != null && !request.Headers.Contains("Authorization"))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
        }

        var response = await base.SendAsync(request, cancellationToken);

        // Check if unauthorized and try refreshing the token
        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            var refreshToken = userSettingsService.GetRefreshToken();
            var username = userSettingsService.GetUserName();

            if (!string.IsNullOrEmpty(refreshToken) && !string.IsNullOrEmpty(username))
            {
                // Attempt token refresh
                var refreshTokenResponse = await authService.Refreshtoken(username, refreshToken);

                if(refreshTokenResponse?.Success ?? false)
                {
                    userSettingsService.SaveToken(refreshTokenResponse?.Data!);

                    // Retry the original request with the refreshed token
                    bearerToken = userSettingsService.GetToken();
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

                    // Retry the request
                    response = await base.SendAsync(request, cancellationToken);
                }
                else
                {
                    windowService.LoadPage(new Login());
                    await windowService.ShowWarningAsync("Your session has expired, please login again.", 4);
                }

            }
        }

        return response;
    }
}
