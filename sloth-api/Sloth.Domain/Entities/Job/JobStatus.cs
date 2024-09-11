namespace Sloth.Domain.Entities;
/// <summary>
/// This table lists all possible statuses, that may be assigned to product
/// </summary>
public class JobStatus
{
    public int StatusID { get; set; }
    /// <summary>
    /// references Product
    /// </summary>
    public int ProductID { get; set; }
    /// <summary>
    /// references JobType
    /// </summary>
    public int JobTypeID { get; set; }
    public string StatusText { get; set; } = default!;
    /// <summary>
    /// references UserRole
    /// </summary>
    public int UserRoleID { get; set; }
}
