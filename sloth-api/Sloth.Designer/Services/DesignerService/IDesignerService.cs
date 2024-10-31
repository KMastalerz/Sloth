using Sloth.Designer.Models;
using Sloth.Shared.Models;

namespace Sloth.Designer.Services;
public interface IDesignerService
{
    Task<ServiceReturnValue<IEnumerable<ListWebPageItem>>?> ListWebPageByID(string? appID, string? pageID);
    Task<ServiceReturnValue<IEnumerable<string>>?> ListWebApplicationIDs();
    Task<ServiceReturnValue<WebPageItem>?> GetFullWebPage(string appID, string pageID);
    Task<ServiceReturnValue<string>?> SaveFullWebPage(WebPageItem webPage);
    Task<ServiceReturnValue<string>?> DeleteWebPage(string appID, string pageID);
}