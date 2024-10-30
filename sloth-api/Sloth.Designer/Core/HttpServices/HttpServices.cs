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
                // Handle string responses differently from JSON-deserializable types
                if (typeof(T) == typeof(string))
                {
                    result.Data = (T)(object)(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    result.Data = await response.Content.ReadFromJsonAsync<T>();
                }
            }
            else
            {
                result.Error = await response.Content.ReadAsStringAsync();
            }
        }
        catch (Exception ex) when (ex is HttpRequestException || ex is TaskCanceledException || ex is AggregateException)
        {
            result.Error = ex switch
            {
                HttpRequestException => $"Request error: {ex.Message}",
                TaskCanceledException => $"Request timed out: {ex.Message}",
                AggregateException ae => $"Aggregate exception: {ae.Flatten().Message}",
                _ => $"Unexpected error: {ex.Message}"
            };
            result.ResponseCode = ex is TaskCanceledException ? 408 : 500;
            result.Success = false;
        }

        return result;
    }
}
