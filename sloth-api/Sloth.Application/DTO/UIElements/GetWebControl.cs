namespace Sloth.Application.DTO;
public class GetWebControl
{
    public string? PanelID { get; set; } = null;
    public string? SectionID { get; set; } = null;
    public string ControlID { get; set; } = default!;
    public string? ControlLabel { get; set; }
    public string? ControlPlaceholder { get; set; }
    public string? ControlTooltip { get; set; }
    public string? Route { get; set; } = null;
    public string? RoutePageID { get; set; } = null;
    public string? MetaData { get; set; } = null;
    public bool IsHidden { get; set; } = false;
    public bool IsReadOnly { get; set; } = false;
    public bool IsDisabled { get; set; } = false;
    public bool IsRequired { get; set; } = false;
}
