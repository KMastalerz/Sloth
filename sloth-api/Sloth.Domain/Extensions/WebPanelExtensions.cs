using Sloth.Domain.Entities;

namespace Sloth.Domain.Extensions;
public static class WebPanelExtensions
{
    public static WebPanel Update(this WebPanel webPanel, WebPanel newValues, Guid userID)
    {
        webPanel.PanelType = newValues.PanelType;
        webPanel.Sections = newValues.Sections;
        webPanel.Controls = newValues.Controls;
        webPanel.Class = newValues.Class;
        webPanel.SecurityTableID = newValues.SecurityTableID;
        webPanel.Label = newValues.Label;
        webPanel.MetaData = newValues.MetaData;
        webPanel.ChangeDate = DateTime.UtcNow;
        webPanel.ChangeUser = userID;
        return webPanel;
    }
}
