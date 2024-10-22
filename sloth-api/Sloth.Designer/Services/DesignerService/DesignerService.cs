using Sloth.Designer.Core;
using Sloth.Shared.Models;
using System.Collections.ObjectModel;

namespace Sloth.Designer.Services;
internal class DesignerService(IHttpServices httpServices) : IDesignerService
{
    public async Task<ObservableCollection<ListWebPageItem>?> ListWebPageByID(string? appID, string? pageID)
    {
        var parms = new Dictionary<string, object?>()
        {
            { "appID", appID },
            { "pageID", pageID }
        };

        var response = await httpServices.GetAsync<IEnumerable<ListWebPageItem>>(HttpServicePaths.UIElements, "ListWebPageByID", parms);
        return response.Data is null ? null : new(response.Data);
    }

    public async Task<IEnumerable<string>?> ListWebApplicationIDs()
    {
        var response = await httpServices.GetAsync<IEnumerable<string>>(HttpServicePaths.UIElements, "ListWebApplicationIDs");
        return response.Data is null ? null : response.Data;
    }
}
