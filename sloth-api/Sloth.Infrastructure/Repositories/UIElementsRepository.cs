using Microsoft.EntityFrameworkCore;
using Sloth.Domain.Entities;
using Sloth.Domain.Repositories;
using Sloth.Infrastructure.DatabaseContext;

namespace Sloth.Infrastructure.Repositories;
internal class UIElementsRepository(SlothDbContext dbContext) : IUIElementsRepository
{
    public async Task<WebPage?> GetWebPageAsync(string PageID)
    {
        var webPage = await dbContext.WebPage
            .Include(wp => wp.WebControls)
                .ThenInclude(wc => wc.WebControlSecurities) // Include WebControlSecurities for each WebControl
            .Include(wp => wp.WebControls)
                .ThenInclude(wc => wc.Validations) // Include Validations for each WebControl through WebControlValidation
            .Include(wp => wp.SecurityTables) // Include related SecurityTables
            .Include(wp => wp.WebPageSecurities) // Include related WebPageSecurities
            .FirstOrDefaultAsync(wp => wp.PageID == PageID); // Fetch the specific WebPage by PageID

        // Fetch Translations where ENUText matches ControlLabel or ControlPlaceholder
        if (webPage != null)
        {
            foreach (var webControl in webPage.WebControls)
            {
                var translations = await dbContext.Translation
                    .Where(t => t.ENUText == webControl.ControlLabel || t.ENUText == webControl.ControlPlaceholder)
                    .ToListAsync();

                // Assign fetched translations to WebControl's Translations property
                webControl.Translations = translations;
            }
        }

        return webPage;
    }
}
