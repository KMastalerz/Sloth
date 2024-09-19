namespace Sloth.Domain.Entities;
public class WebControl
{
    /// <summary>
    /// references WebPage
    /// </summary>
    public string PageID { get; set; } = default!;
    public string ControlID { get; set; } = default!;
    /// <summary>
    /// references SecurityTable, If added and not overriten it will take flags from table security. 
    /// </summary>
    public string? SecurityTableID { get; set; }
    /// <summary>
    /// type protected by constant List<string> ControlTypes;
    /// </summary>
    public string ControlType { get; set; } = default!;
    public string? ControlLabel { get; set; } = null;
    public string? ControlPlaceholder { get; set; } = null;
    public string? ControlTooltip { get; set; } = null;
    public string? Route { get; set; } = null;
    public string? RoutePageID { get; set; } = null;
    public string? Action { get; set; } = null;
    /// <summary>
    /// JSON object, that defines the control's properties
    /// </summary>
    public string? MetaData { get; set; } = null;
}
