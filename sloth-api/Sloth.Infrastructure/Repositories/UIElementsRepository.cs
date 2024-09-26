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
    public async Task<IEnumerable<WebControl>?> ListWebControlAsync(string pageID)
    {
        return await dbContext.WebControl.Where(p => p.PageID == pageID).ToListAsync();;
    }

}
