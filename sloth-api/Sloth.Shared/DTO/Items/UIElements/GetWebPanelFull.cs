namespace Sloth.Shared.DTO;
public class GetWebPanelFull
{
    public string AppID { get; set; } = default!;
    public string PageID { get; set; } = default!;
    public string PanelID { get; set; } = default!;
    public string PanelType { get; set; } = default!;
    public string? Sections { get; set; } = null;
    public string? Class { get; set; } = null;
    public string? SecurityTableID { get; set; } = null;
    public string? Label { get; set; } = null;
    public string? Metadata { get; set; } = null;
    public List<GetWebSectionFull> WebSections { get; set; } = [];
    public DateTime ChangeDate { get; set; } = DateTime.UtcNow!;
    public Guid ChangeUser { get; set; } = default!;
}
