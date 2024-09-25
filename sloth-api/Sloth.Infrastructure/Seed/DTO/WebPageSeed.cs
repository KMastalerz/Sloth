namespace Sloth.Infrastructure.Seed;
internal class WebPageSeed
{
    public string PageID { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string? SecurityTableID { get; set; }
    public string? Description { get; set; }
    public string? UserGroup { get; set; }
    public string? Panels { get; set; } = null;
    public bool CanAccess { get; set; } = true;
    public bool CanAdd { get; set; } = true;
    public bool CanDelete { get; set; } = true;
}
