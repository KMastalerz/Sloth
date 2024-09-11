namespace Sloth.Domain.Entities;
/// <summary>
/// This table represents all current job service level agreements
/// </summary>
public class JobSLA
{
    public int JobSLAID { get; set; }
    /// <summary>
    /// references Product
    /// </summary>
    public int ProductID { get; set; }
    /// <summary>
    /// references JobPriority, since SLA may vary on that. 
    /// </summary>
    public int PriorityID { get; set; }
    /// <summary>
    /// references Client, since SLA may vary on that. 
    /// </summary>
    public int? ClientID { get; set; }
    /// <summary>
    /// references JobType, since SLA may vary on that. 
    /// </summary>
    public int? JobTypeID { get; set; }
    /// <summary>
    /// references reporting Team, since SLA may vary on that. 
    /// </summary>
    public int? ReportingTeamID { get; set; }
    public string AgreementHeader { get; set; } = default!;
    public string AgreementDescription { get; set; } = default!;
    /// <summary>
    /// time for initial response to bug, in minutes
    /// </summary>
    public int? InitialResponseTime { get; set; } = null;
    /// <summary>
    /// time for initial resolution to bug, in minutes
    /// </summary>
    public int? InitialResolutionTime { get; set; } = null;
    /// <summary>
    /// time for final resolution to bug, in minutes
    /// </summary>
    public int? FinalResolutionTime { get; set; } = null;
    /// <summary>
    /// frequency of updates deliveres to client, in minutes
    /// </summary>
    public int? UpdateFrequency { get; set; } = null;
    /// <summary>
    /// how long specific Job/Task has to take, in minutes
    /// </summary>
    public int? WorkTime { get; set; }
    /// <summary>
    /// when this has to be delivered
    /// </summary>
    public DateTime? DeliveryTime { get; set; }
}
