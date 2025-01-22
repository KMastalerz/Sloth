namespace sloth.Application.Models.Jobs;
public class ListJobDataCacheItem
{
    public List<CacheClientItem> Clients { get; set; } = [];
    public List<CachePriorityItem> Priorities { get; set; } = [];
    public List<CacheProductItem> Products { get; set; } = [];
    public List<CacheFunctionalityItem> Functionalities { get; set; } = [];
    public List<CacheStatusItem> Statuses { get; set; } = [];
    public List<CacheAssigneeItem> Assignees { get; set; } = [];
}

public class CacheClientItem
{
    public string Name { get; set; } = default!;
    public Guid? ClientID { get; set; } = null;

    public IEnumerable<CacheProductItem> Products { get; set; } = [];
}

public class CachePriorityItem
{
    public int PriorityID { get; set; }
    public string Tag { get; set; } = default!;
    public string? TagColor { get; set; } = default;
}

public class CacheProductItem
{
    public int ProductID { get; set; }
    public string? Description { get; set; } = null;
    public string Name { get; set; } = default!;
}

public class CacheFunctionalityItem
{
    public int FunctionalityID { get; set; }
    public string Name { get; set; } = default!;
    public string Tag { get; set; } = default!;
    public string? TagColor { get; set; } = null;
    public string? Description { get; set; } = null;
    public string? Product { get; set; } = null;
    public int? ProductID { get; set; } = null;
}

public class CacheStatusItem
{
    public int StatusID { get; set; }
    public string Type { get; set; } = default!;
    public string Tag { get; set; } = default!;
    public string? TagColor { get; set; } = null;
    public string? Description { get; set; } = null;
}

public class CacheAssigneeItem
{
    public string UserName { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string Team {  get; set; } = default!;
    public string Email { get; set; } = default!;
}