namespace Sloth.Domain.Entities;

public class WebPanel
{
    public string PageID { get; set; } = default!;
    public string PanelID { get; set; } = default!;
    public string PanelType { get; set; } = default!;
    public string? SecurityTableID { get; set; } = null;
    public string? PanelLabel { get; set; } = null;
}
