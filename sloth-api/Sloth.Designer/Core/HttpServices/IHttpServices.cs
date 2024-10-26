
namespace Sloth.Designer.Core;

internal interface IHttpServices
{
    Task<ServiceReturnValue<T>> GetAsync<T>(string basePath, string methodName, Dictionary<string, object?> parameters);
    Task<ServiceReturnValue<T>> GetAsync<T>(string basePath, string methodName);
    Task<ServiceReturnValue<T>> PostAsync<T>(string basePath, string methodName, object data);
}