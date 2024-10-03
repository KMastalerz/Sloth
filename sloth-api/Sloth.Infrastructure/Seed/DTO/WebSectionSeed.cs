namespace Sloth.Infrastructure.Seed;

public class WebSectionSeed
{
    public string PageID { get; set; } = default!;
    public string PanelID { get; set; } = default!;
    public string SectionID { get; set; } = default!;
    public string Controls { get; set; } = default!;
    public string? Title { get; set; }
    public string? MetaData { get; set; }
}
