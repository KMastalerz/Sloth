namespace Sloth.Domain.Entities;
public class WebSection
{
    public string PageID { get; set; } = default!;
    public string PanelID { get; set; } = default!;
    public string SectionID { get; set; } = default!;
    public string? SecurityTableID { get; set; } = null;
    // Array | Form => defaults to Form
    public string? SectionType { get; set; } = null;
    public string? SectionLabel { get; set; } = null;
    public string? Controls { get; set; }
}


