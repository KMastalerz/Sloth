namespace Sloth.Infrastructure.Seed;
public class WebPageSecuritySeed
{
    public string PageID { get; set; } = default!;
    public string UserGroup { get; set; } = default!;
    public bool CanAccess { get; set; } = true;
    public bool CanAdd { get; set; } = true;
    public bool CanDelete { get; set; } = true;
}
