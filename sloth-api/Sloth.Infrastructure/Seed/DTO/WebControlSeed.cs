namespace Sloth.Infrastructure.Seed;
internal class WebControlSeed
{
    public string PageID { get; set; } = default!;
    public string PanelID { get; set; } = default!;
    public string SectionID { get; set; } = default!;
    public string ControlID { get; set; } = default!;
    public string ControlType { get; set; } = default!;
    public string? SecurityTableID { get; set; }
    public string? ControlLabel { get; set; } = null;
    public string? ControlPlaceholder { get; set; } = null;
    public string? ControlTooltip { get; set; } = null;
    public string? Route { get; set; } = null;
    public string? RoutePageID { get; set; } = null;
    public string? Action { get; set; } = null;
    public string? MetaData { get; set; } = null;
    public string? Validation { get; set; } = null;
}
