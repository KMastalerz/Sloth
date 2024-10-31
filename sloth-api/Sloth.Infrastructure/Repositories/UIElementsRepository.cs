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
        var webPage =  await dbContext.WebPage
            .Include(p => p.WebPanels)
                .ThenInclude(pa => pa.WebControls) // Include only controls without SectionID
            .Include(p => p.WebPanels)
                .ThenInclude(pa => pa.WebSections) // Include sections inside panels
                    .ThenInclude(s => s.WebControls) // Include controls inside sections
            .FirstOrDefaultAsync(p => p.AppID == appID && p.PageID == pageID);

        if (webPage != null)
        {
            foreach (var panel in webPage.WebPanels)
            {
                panel.WebControls = panel.WebControls.Where(c => c.SectionID == null).ToList();
            }
        }

        return webPage;
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

    public async Task<IEnumerable<string>?> ListWebApplicationIDsAsync()
    {
        return await dbContext.WebPage.Select(p => p.AppID).Distinct().ToListAsync();
    }

    public async Task SaveWebPageAsync(WebPage webPage)
    {
        dbContext.WebPage.Update(webPage);
        await dbContext.SaveChangesAsync();
    }

    public Task SaveWebPanelAsync(WebPanel webPanel)
    {
        dbContext.WebPanel.Update(webPanel);
        return dbContext.SaveChangesAsync();
    }

    public Task SaveWebSectionAsync(WebSection webSection)
    {
        dbContext.WebSection.Update(webSection);
        return dbContext.SaveChangesAsync();
    }

    public Task SaveWebControlAsync(WebControl webControl)
    {
        dbContext.WebControl.Update(webControl);
        return dbContext.SaveChangesAsync();
    }

    public Task AddWebPageAsync(WebPage webPage)
    {
        dbContext.WebPage.Add(webPage);
        return dbContext.SaveChangesAsync();
    }

    public Task AddWebPanelAsync(WebPanel webPanel)
    {
        dbContext.WebPanel.Add(webPanel);
        return dbContext.SaveChangesAsync();
    }

    public Task AddWebSectionAsync(WebSection webSection)
    {
        dbContext.WebSection.Add(webSection);
        return dbContext.SaveChangesAsync();
    }

    public Task AddWebControlAsync(WebControl webControl)
    {
        dbContext.WebControl.Add(webControl);
        return dbContext.SaveChangesAsync();
    }

    public Task RemoveWebPageAsync(WebPage webPage)
    {
        dbContext.WebPage.Remove(webPage);
        return dbContext.SaveChangesAsync();
    }

    public Task RemoveWebPanelAsync(WebPanel webPanel)
    {
        dbContext.WebPanel.Remove(webPanel);
        return dbContext.SaveChangesAsync();
    }

    public Task RemoveWebSectionAsync(WebSection webSection)
    {
        dbContext.WebSection.Remove(webSection);
        return dbContext.SaveChangesAsync();
    }

    public Task RemoveWebControlAsync(WebControl webControl)
    {
        dbContext.WebControl.Remove(webControl);
        return dbContext.SaveChangesAsync();
    }
}
