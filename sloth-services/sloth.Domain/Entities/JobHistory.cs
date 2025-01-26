namespace sloth.Domain.Entities;
public class JobHistory
{
    public int JobID { get; set; }
    public Guid ChangedByID { get; set; }
    public DateTime ChangeDate { get; set; }
    /// <summary>
    /// Accepts:
    /// - ChildJob
    /// - Client
    /// - Description
    /// - Functionality
    /// - Header
    /// - ParentJob
    /// - Priority
    /// - Product
    /// - Status
    /// </summary>
    public string Type { get; set; } = default!;
    /// <summary>
    /// Accepts: 
    /// - Add
    /// - Change
    /// - Delete
    /// </summary>
    public string Action { get; set; } = default!;
    public string Value { get; set; } = default!;

    /// <summary>
    /// External properties
    /// </summary>
    public User ChangedBy { get; set; } = default!;
} 
