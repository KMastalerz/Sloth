namespace Sloth.Domain.Entities;
/// <summary>
/// This table holds all possible to add priorities, they are also protected by UserRole, which means that specific user has to have this role, to apply this status.
/// PriorityCode, indicates severity of status for example: 1 - low, 2 - medium, 3 - high, 4 - critical. (We can name them, however we want)
/// </summary>
internal class ServicePriority
{
    public int PriorityID { get; set; }
    /// <summary>
    /// references Service
    /// </summary>
    public int ServiceID { get; set; } 
    public string PriorityText { get; set; } = default!;
    public int PriorityCode { get; set; }
    /// <summary>
    /// references UserRole
    /// </summary>
    public int UserRoleID { get; set; } 
}
