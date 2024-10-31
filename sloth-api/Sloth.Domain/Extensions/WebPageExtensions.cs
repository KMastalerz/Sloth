using Sloth.Domain.Entities;

namespace Sloth.Domain.Extensions;
public static class WebPageExtensions
{
    public static WebPage Update(this WebPage webPage, WebPage newValues, Guid userID)
    {
        webPage.Label = newValues.Label;
        webPage.Panels = newValues.Panels;
        webPage.Orientation = newValues.Orientation;
        webPage.Background = newValues.Background;
        webPage.Position = newValues.Position;
        webPage.Class = newValues.Class;
        webPage.HasRouter = newValues.HasRouter;
        webPage.SecurityTableID = newValues.SecurityTableID;
        webPage.Description = newValues.Description;
        webPage.MetaData = newValues.MetaData;
        webPage.ChangeDate = DateTime.UtcNow;
        webPage.ChangeUser = userID;
        return webPage;
    }
}
