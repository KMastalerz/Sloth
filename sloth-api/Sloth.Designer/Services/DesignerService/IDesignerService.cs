using Sloth.Designer.Models;

namespace Sloth.Designer.Services;
public interface IDesignerService
{
    Task<ServiceReturnValue<IEnumerable<ListWebPageItem>>?> ListWebPageByID(string? appID, string? pageID);
    Task<ServiceReturnValue<IEnumerable<string>>?> ListWebApplicationIDs();
    Task<ServiceReturnValue<WebPageItem>?> GetFullWebPage(string appID, string pageID);
    Task<ServiceReturnValue<WebPageItem>?> SaveFullWebPage(WebPageItem webPage);
    Task<ServiceReturnValue<string>?> DeleteWebPage(string appID, string pageID);
    Task<ServiceReturnValue<ListWebPagesItem>?> ListWebPages();
}