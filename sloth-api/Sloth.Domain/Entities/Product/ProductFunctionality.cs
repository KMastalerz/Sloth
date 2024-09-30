namespace Sloth.Domain.Entities;
/// <summary>
/// This table represents functionalities within product like: Payments, Timesheet etc... 
/// </summary>
public class ProductFunctionality
{
    public Guid FunctionalityID { get; set; }
    /// <summary>
    /// references Product
    /// </summary>
    public Guid ProductID { get; set; }
    public string FunctionalityName { get; set;} = default!;
    public string Description { get; set; } = default!;
}
