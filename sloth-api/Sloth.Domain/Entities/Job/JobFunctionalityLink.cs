namespace Sloth.Domain.Entities;
/// <summary>
/// This table allows to link job, to specific functionality within the product
/// </summary>
public class JobFunctionalityLink
{
    /// <summary>
    /// references Job
    /// </summary>
    public int JobID { get; set; }
    /// <summary>
    /// references ProductFunctionality
    /// </summary>
    public int FunctionalityID { get; set; }
}
