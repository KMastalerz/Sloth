using Sloth.Designer.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace Sloth.Designer.Helpers;
public static class HttpHelper
{
    public static async Task<ServiceReturnValue<T>> GetAsync<T>(this HttpClient httpClient, string basePath, string methodName, Dictionary<string, object?>? parameters = null)
    {
        var request = parameters != null
        ? RequestBuilder.BuildRequest(basePath, methodName, parameters)
        : RequestBuilder.BuildRequest(basePath, methodName);

        var result = new ServiceReturnValue<T>();

        try
        {
            var response = await httpClient.GetAsync(request);
            result.ResponseCode = (int)response.StatusCode;
            result.Success = response.IsSuccessStatusCode;

            if (response.IsSuccessStatusCode)
            {
                result.Data = await response.ParseResponse<T>();
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

    public static async Task<ServiceReturnValue<T>> PostAsync<T>(this HttpClient httpClient, string basePath, string methodName, object data)
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
                result.Data = await response.ParseResponse<T>();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessage = JsonSerializer.Deserialize<Dictionary<string, string>>(errorContent)?["error"];
                result.Error = errorMessage; // Assign only the message part
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

    public static async Task<ServiceReturnValue<T>> DeleteAsync<T>(this HttpClient httpClient, string basePath, string methodName, Dictionary<string, object?>? parameters = null)
    {
        var request = parameters != null
            ? RequestBuilder.BuildRequest(basePath, methodName, parameters)
            : RequestBuilder.BuildRequest(basePath, methodName);

        var result = new ServiceReturnValue<T>();

        try
        {
            var response = await httpClient.DeleteAsync(request);
            result.ResponseCode = (int)response.StatusCode;
            result.Success = response.IsSuccessStatusCode;

            if (response.IsSuccessStatusCode)
            {
                result.Data = await response.ParseResponse<T>();
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

    private class RequestBuilder
    {
        public static string BuildRequest(string basePath, string request, Dictionary<string, object?>? parms)
        {

            if (parms == null || !parms.Any() || parms.All(p => p.Value == null))
                return $"{basePath}/{request}";

            var queryString = string.Join("&", parms
                .Where(param => param.Value != null) // Skip null values
                .Select(param => $"{Uri.EscapeDataString(param.Key)}={Uri.EscapeDataString(Convert.ToString(param.Value, System.Globalization.CultureInfo.InvariantCulture)!)}"));

            return $"{basePath}/{request}?{queryString}";
        }
        public static string BuildRequest(string basePath, string request)
        {
            return $"{basePath}/{request}";
        }
    }

    private static async Task<T> ParseResponse<T>(this HttpResponseMessage response)
    {
        var responseString = await response.Content.ReadAsStringAsync();

        return typeof(T) switch
        {
            var t when t == typeof(string) => (T)(object)responseString,
            var t when t == typeof(decimal) => (T)(object)decimal.Parse(responseString),
            var t when t == typeof(double) => (T)(object)double.Parse(responseString),
            var t when t == typeof(int) => (T)(object)int.Parse(responseString),
            var t when t == typeof(long) => (T)(object)long.Parse(responseString),
            var t when t == typeof(bool) => (T)(object)bool.Parse(responseString),
            var t when t == typeof(DateTime) => (T)(object)DateTime.Parse(responseString),
            _ => await response.Content.ReadFromJsonAsync<T>() ?? throw new InvalidOperationException("Unable to parse response")
        };
    }
}
