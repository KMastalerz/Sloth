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
    Task SaveWebPageAsync(WebPage webPage);
    Task SaveWebPanelAsync(WebPanel webPanel);
    Task SaveWebSectionAsync(WebSection webSection);
    Task SaveWebControlAsync(WebControl webControl);
    Task AddWebPageAsync(WebPage webPage);
    Task AddWebPanelAsync(WebPanel webPanel);
    Task AddWebSectionAsync(WebSection webSection);
    Task AddWebControlAsync(WebControl webControl);
    Task RemoveWebPageAsync(WebPage webPage);
    Task RemoveWebPanelAsync(WebPanel webPanel);
    Task RemoveWebSectionAsync(WebSection webSection);
    Task RemoveWebControlAsync(WebControl webControl);
}