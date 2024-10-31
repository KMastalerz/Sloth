using Sloth.Domain.Entities;

namespace Sloth.Domain.Extensions;
public static class WebControlExtensions
{
    public static WebControl Update(this WebControl webControl, WebControl newValues, Guid userID)
    {
        webControl.ControlType = newValues.ControlType;
        webControl.InnerType = newValues.InnerType;
        webControl.Style = newValues.Style;
        webControl.Size = newValues.Size;
        webControl.Controls = newValues.Controls;
        webControl.SecurityTableID = newValues.SecurityTableID;
        webControl.Label = newValues.Label;
        webControl.Placeholder = newValues.Placeholder;
        webControl.Tooltip = newValues.Tooltip;
        webControl.TooltipPosition = newValues.TooltipPosition;
        webControl.Route = newValues.Route;
        webControl.RoutePageID = newValues.RoutePageID;
        webControl.Action = newValues.Action;
        webControl.Icon = newValues.Icon;
        webControl.MetaData = newValues.MetaData;
        webControl.Validation = newValues.Validation;
        webControl.ChangeDate = DateTime.UtcNow;
        webControl.ChangeUser = userID;
        return webControl;
    }
}
