

namespace Sloth.Domain.Entities;
public class WebPage
{
    public string PageID { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string? SecurityTableID { get; set; }
    public string? Description { get; set; }
}
