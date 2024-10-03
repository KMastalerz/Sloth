namespace Sloth.Domain.Entities;
public class WebPanel
{
    public string PageID { get; set; } = default!;
    public string PanelID { get; set; } = default!;
    public string PanelType { get; set; } = default!;
    public string Sections { get; set; } = default!;
    public string? Title { get; set; } = null;
    public string? MetaData { get; set; } = null;
}
