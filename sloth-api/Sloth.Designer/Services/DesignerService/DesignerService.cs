using Sloth.Designer.Models;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;

namespace Sloth.Designer.Services;
internal class DesignerService : IDesignerService
{
    private readonly HttpClient httpClient;
    public DesignerService(IHttpClientFactory httpClientFactory)
    {
        httpClient = httpClientFactory.CreateClient("ApiClient");
    }
    private static string basePath = "/api/UIElements/";
    public async Task<ObservableCollection<ListWebPageItem>?> ListWebPageByID(string? appID, string? pageID)
    {
        var request = RequestBuilder.BuildRequest(basePath, "ListWebPageByID", new()
        {
            { "appID", appID },
            { "pageID", pageID }
        });
        var response = await httpClient.GetAsync(request);

        if (response.IsSuccessStatusCode)
        {
            var webPages = await response.Content.ReadFromJsonAsync<IEnumerable<ListWebPageItem>>();
            return webPages is null ? null : new(webPages);
        }
        else
        {
            throw new NotImplementedException();
        }
    }
}
