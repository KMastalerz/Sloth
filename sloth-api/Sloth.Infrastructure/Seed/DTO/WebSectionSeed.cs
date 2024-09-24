namespace Sloth.Infrastructure.Seed;
internal class WebSectionSeed
{
    public string PageID { get; set; } = default!;
    public string PanelID { get; set; } = default!;
    public string SectionID { get; set; } = default!;
    public string? SecurityTableID { get; set; } = null;
    public string SectionType { get; set; } = default!;
    public string? SectionLabel { get; set; } = null;
}
