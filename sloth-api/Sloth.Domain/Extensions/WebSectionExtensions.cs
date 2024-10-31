using Sloth.Domain.Entities;

namespace Sloth.Domain.Extensions;
public static class WebSectionExtensions
{
    public static WebSection Update(this WebSection webSection, WebSection newValues, Guid userID)
    {
        webSection.Controls = newValues.Controls;
        webSection.Label = newValues.Label;
        webSection.MetaData = newValues.MetaData;
        webSection.ChangeDate = DateTime.UtcNow;
        webSection.ChangeUser = userID;
        return webSection;
    }
}
