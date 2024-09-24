using Microsoft.EntityFrameworkCore;
using Sloth.Domain.Entities;
using Sloth.Domain.Repositories;
using Sloth.Infrastructure.DatabaseContext;

namespace Sloth.Infrastructure.Repositories;
internal class UIElementsRepository(SlothDbContext dbContext) : IUIElementsRepository
{
    public async Task<WebPage?> GetWebPageAsync(string pageID)
    {
        return await dbContext.WebPage.FindAsync(pageID);
    }
    public async Task<IEnumerable<WebPanel>?> ListWebPanelAsync(string pageID)
    {
        return await dbContext.WebPanel.Where(wp => wp.PageID == pageID).ToListAsync();
    }
    public async Task<IEnumerable<WebSection>?> ListWebSectionAsync(string pageID)
    {
        return await dbContext.WebSection.Where(wp => wp.PageID == pageID).ToListAsync();
    }
    public async Task<IEnumerable<WebControl>?> ListWebControlAsync(string pageID)
    {
        return await dbContext.WebControl.Where(p => p.PageID == pageID).ToListAsync();;
    }

}
