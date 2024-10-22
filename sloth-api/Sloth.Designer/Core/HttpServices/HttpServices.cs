
using System.Net.Http;
using System.Net.Http.Json;

namespace Sloth.Designer.Core;

internal class HttpServices : IHttpServices
{

    private readonly HttpClient httpClient;
    public HttpServices(IHttpClientFactory httpClientFactory)
    {
        httpClient = httpClientFactory.CreateClient("ApiClient");
    }

    public async Task<ServiceReturnValue<T>> GetAsync<T>(string basePath, string methodName, Dictionary<string, object?> parameters)
    {
        var request = RequestBuilder.BuildRequest(basePath, methodName, parameters);
        var response = await httpClient.GetAsync(request);

        var result = new ServiceReturnValue<T>
        {
            ResponseCode = (int)response.StatusCode,
            Success = response.IsSuccessStatusCode
        };

        if (response.IsSuccessStatusCode)
        {
            // Successfully received the data
            result.Data = await response.Content.ReadFromJsonAsync<T>();
        }
        else
        {
            // In case of error, populate error details
            result.Error = await response.Content.ReadAsStringAsync(); // Capture error message
        }

        return result;
    }
    public async Task<ServiceReturnValue<T>> GetAsync<T>(string basePath, string methodName)
    {
        var request = RequestBuilder.BuildRequest(basePath, methodName);
        var response = await httpClient.GetAsync(request);

        var result = new ServiceReturnValue<T>
        {
            ResponseCode = (int)response.StatusCode,
            Success = response.IsSuccessStatusCode
        };

        if (response.IsSuccessStatusCode)
        {
            // Successfully received the data
            result.Data = await response.Content.ReadFromJsonAsync<T>();
        }
        else
        {
            // In case of error, populate error details
            result.Error = await response.Content.ReadAsStringAsync(); // Capture error message
        }

        return result;
    }
}
