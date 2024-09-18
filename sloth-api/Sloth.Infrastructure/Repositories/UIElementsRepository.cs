using Microsoft.EntityFrameworkCore;
using Sloth.Domain.Entities;
using Sloth.Domain.Exceptions;
using Sloth.Domain.Repositories;
using Sloth.Infrastructure.DatabaseContext;

namespace Sloth.Infrastructure.Repositories;
internal class UIElementsRepository(SlothDbContext dbContext) : IUIElementsRepository
{
    public async Task<WebPage?> GetWebPageAsync(string PageID, string UserGroup)
    {
        // Check if user can access this page, as WebPage has to co-exist with WebPageSecurity this is a valid check
        if (!dbContext.WebPageSecurity.Any(p => p.CanAccess == true && p.UserGroup == UserGroup)) 
            throw new MissingAccessException(nameof(WebPage), PageID);

        var webPage = await dbContext.WebPage.FindAsync(PageID);
        return webPage;
    }

    public async Task<IEnumerable<WebControl>?> ListWebControlAsync(string PageID, string UserGroup)
    {
        var webControls = await dbContext.WebControl.Where(p => p.PageID == PageID).ToListAsync();
        return webControls;
    }

    public async Task<IEnumerable<SecurityTable>?> ListControlSecurityAsync(string PageID, string UserGroup, string? PageSecurityTableID)
    {
        var secTables = await dbContext.SecurityTable.Where(
            p => 
            p.UserGroup == UserGroup &&
            dbContext.WebControl.Any(
                wc => 
                wc.PageID == PageID &&
                wc.ControlID == p.ControlID &&
                wc.SecurityTableID != null ? wc.SecurityTableID == p.SecurityTableID : PageSecurityTableID != null ? PageSecurityTableID == p.SecurityTableID : false)).ToListAsync();

        return secTables;
    }
}
