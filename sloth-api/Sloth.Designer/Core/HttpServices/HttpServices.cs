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
        var result = new ServiceReturnValue<T>();

        try
        {
            var response = await httpClient.GetAsync(request);
            result.ResponseCode = (int)response.StatusCode;
            result.Success = response.IsSuccessStatusCode;

            if (response.IsSuccessStatusCode)
            {
                result.Data = await response.Content.ReadFromJsonAsync<T>();
            }
            else
            {
                result.Error = await response.Content.ReadAsStringAsync();
            }
        }
        catch (HttpRequestException ex)
        {
            result.Error = $"Request error: {ex.Message}";
            result.ResponseCode = 500;
            result.Success = false;
        }
        catch (TaskCanceledException ex)
        {
            result.Error = $"Request timed out: {ex.Message}";
            result.ResponseCode = 408; // Request Timeout
            result.Success = false;
        }
        catch (Exception ex)
        {
            result.Error = $"Unexpected error: {ex.Message}";
            result.ResponseCode = 500;
            result.Success = false;
        }

        return result;
    }

    public async Task<ServiceReturnValue<T>> GetAsync<T>(string basePath, string methodName)
    {
        var request = RequestBuilder.BuildRequest(basePath, methodName);
        var result = new ServiceReturnValue<T>();

        try
        {
            var response = await httpClient.GetAsync(request);
            result.ResponseCode = (int)response.StatusCode;
            result.Success = response.IsSuccessStatusCode;

            if (response.IsSuccessStatusCode)
            {
                result.Data = await response.Content.ReadFromJsonAsync<T>();
            }
            else
            {
                result.Error = await response.Content.ReadAsStringAsync();
            }
        }
        catch (HttpRequestException ex)
        {
            result.Error = $"Request error: {ex.Message}";
            result.ResponseCode = 500;
            result.Success = false;
        }
        catch (TaskCanceledException ex)
        {
            result.Error = $"Request timed out: {ex.Message}";
            result.ResponseCode = 408; // Request Timeout
            result.Success = false;
        }
        catch (Exception ex)
        {
            result.Error = $"Unexpected error: {ex.Message}";
            result.ResponseCode = 500;
            result.Success = false;
        }

        return result;
    }

    public async Task<ServiceReturnValue<T>> PostAsync<T>(string basePath, string methodName, object data)
    {
        var request = RequestBuilder.BuildRequest(basePath, methodName);
        var result = new ServiceReturnValue<T>();

        try
        {
            var response = await httpClient.PostAsJsonAsync(request, data);
            result.ResponseCode = (int)response.StatusCode;
            result.Success = response.IsSuccessStatusCode;

            if (response.IsSuccessStatusCode)
            {
                result.Data = await response.Content.ReadFromJsonAsync<T>();
            }
            else
            {
                result.Error = await response.Content.ReadAsStringAsync();
            }
        }
        catch (HttpRequestException ex)
        {
            result.Error = $"Request error: {ex.Message}";
            result.ResponseCode = 500;
            result.Success = false;
        }
        catch (TaskCanceledException ex)
        {
            result.Error = $"Request timed out: {ex.Message}";
            result.ResponseCode = 408; // Request Timeout
            result.Success = false;
        }
        catch (AggregateException ex) // Specific handling for wrapped exceptions
        {
            result.Error = $"Aggregate exception: {ex.Flatten().Message}";
            result.ResponseCode = 500;
            result.Success = false;
        }
        catch (Exception ex)
        {
            result.Error = $"Unexpected error: {ex.Message}";
            result.ResponseCode = 500;
            result.Success = false;
        }

        return result;
    }
}
