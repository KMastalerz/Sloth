namespace Sloth.Shared.Models;

public class WebControlItem
{
    public string AppID { get; set; } = default!;
    public string PageID { get; set; } = default!;
    public string PanelID { get; set; } = default!;
    public string? SectionID { get; set; } = null;
    public string ControlID { get; set; } = default!;
    public string ControlType { get; set; } = default!;
    public string? InnerType { get; set; } = null;
    public string? ChildControls { get; set; } = null;
    public string? SecurityTableID { get; set; }
    public string? ControlLabel { get; set; } = null;
    public string? ControlPlaceholder { get; set; } = null;
    public string? ControlTooltip { get; set; } = null;
    public string? Route { get; set; } = null;
    public string? RoutePageID { get; set; } = null;
    public string? Action { get; set; } = null;
    public string? Icon { get; set; } = null;
    public string? MetaData { get; set; } = null;
    public string? Validation { get; set; } = null;
    public DateTime ChangeDate { get; set; } = DateTime.UtcNow!;
    public Guid ChangeUser { get; set; } = default!;
}
