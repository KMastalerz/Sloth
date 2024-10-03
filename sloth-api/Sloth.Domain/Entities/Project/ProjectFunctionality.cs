namespace Sloth.Domain.Entities;
/// <summary>
/// This table represents functionalities within project like: Payments, Timesheet etc... 
/// </summary>
public class ProjectFunctionality
{
    public Guid FunctionalityID { get; set; }
    /// <summary>
    /// references Project
    /// </summary>
    public Guid ProjectID { get; set; }
    public string FunctionalityName { get; set;} = default!;
    public string Description { get; set; } = default!;
}
