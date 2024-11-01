using System.Collections.ObjectModel;

namespace Sloth.Shared.Models;

public class WebPageItem
{
    public string AppID { get; set; } = default!;
    public string PageID { get; set; } = default!;
    public string? Label { get; set; } = null;
    public string? Panels { get; set; } = null;
    public string? Orientation { get; set; } = null;
    public string? Background { get; set; } = null;
    public string? Position { get; set; } = null;
    public string? Class { get; set; } = null;
    public bool HasRouter { get; set; } = default!;
    public string? SecurityTableID { get; set; } = null;
    public string? Description { get; set; } = null;
    public string? MetaData { get; set; } = null;
    public ObservableCollection<WebPanelItem> WebPanels { get; set; } = [];
}
