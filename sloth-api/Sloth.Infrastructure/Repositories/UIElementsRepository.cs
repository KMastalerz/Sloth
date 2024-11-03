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
    public async Task<WebPage?> GetFullWebPageAsync(string appID, string pageID)
    {
        return await dbContext.WebPage
            .Include(p => p.WebPanels)
                .ThenInclude(pa => pa.WebSections) // Include sections inside panels
                    .ThenInclude(s => s.WebControls) // Include controls inside sections
            .FirstOrDefaultAsync(p => p.AppID == appID && p.PageID == pageID);
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

    #region [Designer]
    public async Task<IEnumerable<WebPage>?> ListWebPageByIDAsync(string? appID, string? pageID)
    {
        var records = await dbContext.WebPage.Where(p => (appID == null || p.AppID.Contains(appID)) && (pageID == null || p.PageID.Contains(pageID))).ToListAsync();await dbContext.SaveChangesAsync();
        return await dbContext.WebPage.Where(p => (appID == null || p.AppID.Contains(appID)) && (pageID == null || p.PageID.Contains(pageID))).ToListAsync();
    }

    public async Task<IEnumerable<string>?> ListWebApplicationIDsAsync()
    {
        return await dbContext.WebPage.Select(p => p.AppID).Distinct().ToListAsync();
    }

    public async Task SaveWebPageAsync(WebPage webPage)
    {
        dbContext.WebPage.Update(webPage);
        await dbContext.SaveChangesAsync();
    }

    public async Task SaveWebPanelAsync(WebPanel webPanel)
    {
        dbContext.WebPanel.Update(webPanel);
        await dbContext.SaveChangesAsync();
    }

    public async Task SaveWebSectionAsync(WebSection webSection)
    {
        dbContext.WebSection.Update(webSection);
        await dbContext.SaveChangesAsync();
    }

    public async Task SaveWebControlAsync(WebControl webControl)
    {
        dbContext.WebControl.Update(webControl);
        await dbContext.SaveChangesAsync();
    }

    public async Task AddWebPageAsync(WebPage webPage)
    {
        dbContext.WebPage.Add(webPage);
        await dbContext.SaveChangesAsync();
    }

    public async Task AddWebPanelAsync(WebPanel webPanel)
    {
        dbContext.WebPanel.Add(webPanel);
        await dbContext.SaveChangesAsync();
    }

    public async Task AddWebSectionAsync(WebSection webSection)
    {
        dbContext.WebSection.Add(webSection);
        await dbContext.SaveChangesAsync();
    }

    public async Task AddWebControlAsync(WebControl webControl)
    {
        dbContext.WebControl.Add(webControl);
        await dbContext.SaveChangesAsync();
    }

    public async Task RemoveWebPageAsync(WebPage webPage)
    {
        dbContext.WebPage.Remove(webPage);
        await dbContext.SaveChangesAsync();
    }

    public async Task RemoveWebPanelAsync(WebPanel webPanel)
    {
        dbContext.WebPanel.Remove(webPanel);
        await dbContext.SaveChangesAsync();
    }

    public async Task RemoveWebSectionAsync(WebSection webSection)
    {
        dbContext.WebSection.Remove(webSection);
        await dbContext.SaveChangesAsync();
    }

    public async Task RemoveWebControlAsync(WebControl webControl)
    {
        dbContext.WebControl.Remove(webControl);
        await dbContext.SaveChangesAsync();
    }

    public async Task<bool> CheckWebPageExistsAsync(string appID, string pageID)
    {
        return await dbContext.WebPage.AnyAsync(p => p.AppID == appID && p.PageID == pageID);
    }

    public async Task<IEnumerable<WebPage>> ListAllWebPageAsync()
    {
        return await dbContext.WebPage.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<WebPanel>> ListAllWebPanelAsync()
    {
        return await dbContext.WebPanel.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<WebSection>> ListAllWebSectionAsync()
    {
        return await dbContext.WebSection.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<WebControl>> ListAllWebControlAsync()
    {
        return await dbContext.WebControl.AsNoTracking().ToListAsync();
    }

    #endregion
}
