using sloth.Application.Models.Miscellaneous;

namespace sloth.Application.Models.Jobs;
public class ListJobDataCacheItem
{
    public List<ListItem> Products { get; set; } = [];
    public List<ListItem> Clients { get; set; } = [];
    public List<ToggleItem> Priorities { get; set; } = [];
    public List<ListItem> Functionalities { get; set; } = [];
}
