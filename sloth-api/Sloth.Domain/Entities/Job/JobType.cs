namespace Sloth.Domain.Entities;
/// <summary>
/// This table represents lists of types of jobs there can be like:  QA/Client/RTS/Blocker/Enhancement/Project etc... 
/// </summary>
public class JobType
{
    public int JobTypeID { get; set; }
    /// <summary>
    /// references Product table
    /// </summary>
    public int ProductID { get; set; }
    public string TypeText { get; set; } = default!;
}
