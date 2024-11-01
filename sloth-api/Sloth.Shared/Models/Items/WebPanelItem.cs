using System.Collections.ObjectModel;

namespace Sloth.Shared.Models;

public class WebPanelItem
{
    public string AppID { get; set; } = default!;
    public string PageID { get; set; } = default!;
    public string PanelID { get; set; } = default!;
    public string PanelType { get; set; } = default!;
    public string? Sections { get; set; } = null;
    public string? Class { get; set; } = null;
    public string? SecurityTableID { get; set; } = null;
    public string? Label { get; set; } = null;
    public string? MetaData { get; set; } = null;
    public DateTime ChangeDate { get; set; } = DateTime.UtcNow!;
    public Guid ChangeUser { get; set; } = default!;
    public ObservableCollection<WebSectionItem> WebSections { get; set; } = [];
}
