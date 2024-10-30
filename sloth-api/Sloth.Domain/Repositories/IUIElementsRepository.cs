using Sloth.Domain.Entities;

namespace Sloth.Domain.Repositories;

public interface IUIElementsRepository
{
    Task<WebPage?> GetWebPageAsync(string appID, string pageID);
    Task<WebPage?> GetFullWebPageAsync(string appID, string pageID);
    Task<IEnumerable<WebControl>?> ListWebControlAsync(string appID, string pageID);
    Task<IEnumerable<WebPanel>?> ListWebPanelAsync(string appID, string pageID);
    Task<IEnumerable<WebSection>?> ListWebSectionAsync(string appID, string pageID);
    Task<IEnumerable<WebPage>?> ListWebPageByIDAsync(string? appID, string? pageID);
    Task<IEnumerable<string>?> ListWebApplicationIDsAsync();
    Task SaveFullWebPageAsync(WebPage webPage);
}