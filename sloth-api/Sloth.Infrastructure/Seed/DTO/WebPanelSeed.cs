namespace Sloth.Infrastructure.Seed;
public class WebPanelSeed
{
    public string PageID { get; set; } = default!;
    public string PanelID { get; set; } = default!;
    public string PanelType { get; set; } = default!;
    public string? Sections { get; set; } = null;
    public string? Title { get; set; } = null;
    public string? MetaData { get; set; } = null;
}
