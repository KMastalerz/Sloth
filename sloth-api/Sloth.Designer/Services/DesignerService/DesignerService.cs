using Sloth.Designer.Constants;
using Sloth.Designer.Helpers;
using Sloth.Designer.Models;
using Sloth.Shared.Models;
using System.Net.Http;

namespace Sloth.Designer.Services;
internal class DesignerService : IDesignerService
{
    private readonly HttpClient httpClient;
    public DesignerService(IHttpClientFactory httpClientFactory)
    {
        httpClient = httpClientFactory.CreateClient("ApiClient");
    }
    public async Task<ServiceReturnValue<IEnumerable<ListWebPageItem>>?> ListWebPageByID(string? appID, string? pageID)
    {
        var parms = new Dictionary<string, object?>()
        {
            { "appID", appID },
            { "pageID", pageID }
        };

        var response = await httpClient.GetAsync<IEnumerable<ListWebPageItem>>(HttpServicePaths.UIElements, "ListWebPageByID", parms);
        return response ;
    }

    public async Task<ServiceReturnValue<IEnumerable<string>>?> ListWebApplicationIDs()
    {
        var response = await httpClient.GetAsync<IEnumerable<string>>(HttpServicePaths.UIElements, "ListWebApplicationIDs");
        return response;
    }

    public async Task<ServiceReturnValue<WebPageItem>?> GetFullWebPage(string appID, string pageID)
    {
        var parms = new Dictionary<string, object?>()
        {
            { "appID", appID },
            { "pageID", pageID }
        };

        var response = await httpClient.GetAsync<WebPageItem>(HttpServicePaths.UIElements, "GetFullWebPage", parms);
        return response;
    }

    public async Task<ServiceReturnValue<string>?> SaveFullWebPage(WebPageItem webPage)
    {
        var response = await httpClient.PostAsync<string>(HttpServicePaths.UIElements, "SaveFullWebPage", webPage);
        return response;
    }

    public async Task<ServiceReturnValue<string>?> DeleteWebPage(string appID, string pageID)
    {
        var parms = new Dictionary<string, object?>()
        {
            { "appID", appID },
            { "pageID", pageID }
        };

        var response = await httpClient.DeleteAsync<string>(HttpServicePaths.UIElements, "DeleteWebPage", parms);
        return response;
    }
}
