namespace Sloth.Application.DTO;
public class GetWebControl
{
    public string PanelID { get; set; } = default!;
    public string ControlID { get; set; } = default!;
    public string SectionID { get; set; } = default!;
    public string ControlType { get; set; } = default!;
    public string? InnerType { get; set; } = null;
    public string? Style { get; set; } = null;
    public string? Size { get; set; } = null;
    public string? Label { get; set; } = null;
    public string? Placeholder { get; set; } = null;
    public string? Tooltip { get; set; } = null;
    public string? TooltipPosition { get; set; } = null;
    public string? Route { get; set; } = null;
    public string? RoutePageID { get; set; } = null;
    public string? Action { get; set; } = null;
    public string? Icon { get; set; } = null;
    public string? Metadata { get; set; } = null;
    public bool IsHidden { get; set; } = false;
    public bool IsReadOnly { get; set; } = false;
    public bool IsDisabled { get; set; } = false;
    public bool IsRequired { get; set; } = false;
}
