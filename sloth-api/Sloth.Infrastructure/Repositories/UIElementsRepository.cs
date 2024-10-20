using Microsoft.EntityFrameworkCore;
using Sloth.Domain.Entities;
using Sloth.Domain.Repositories;
using Sloth.Infrastructure.DatabaseContext;

namespace Sloth.Infrastructure.Repositories;
internal class UIElementsRepository(SlothDbContext dbContext) : IUIElementsRepository
{
    public async Task<WebPage?> GetWebPageAsync(string appID, string pageID)
    {
        return await dbContext.WebPage.FindAsync(appID, pageID);
    }
    public async Task<IEnumerable<WebControl>?> ListWebControlAsync(string appID, string pageID)
    {
        return await dbContext.WebControl.Where(p => p.AppID == appID && p.PageID == pageID).ToListAsync();;
    }
    public async Task<IEnumerable<WebPanel>?> ListWebPanelAsync(string appID, string pageID)
    {
        return await dbContext.WebPanel.Where(p => p.AppID == appID && p.PageID == pageID).ToListAsync(); ;
    }
    public async Task<IEnumerable<WebSection>?> ListWebSectionAsync(string appID, string pageID)
    {
        return await dbContext.WebSection.Where(p => p.AppID == appID && p.PageID == pageID).ToListAsync(); ;
    }
    public async Task<IEnumerable<WebPage>?> ListWebPageByIDAsync(string? appID, string? pageID)
    {
        var records = await dbContext.WebPage.Where(p => (appID == null || p.AppID.Contains(appID)) && (pageID == null || p.PageID.Contains(pageID))).ToListAsync();
        return await dbContext.WebPage.Where(p => (appID == null || p.AppID.Contains(appID)) && (pageID == null || p.PageID.Contains(pageID))).ToListAsync();
    }
}
