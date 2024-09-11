namespace Sloth.Domain.Entities;
/// <summary>
/// This table represents functionalities within product like: Payments, Timesheet etc... 
/// </summary>
public class ProductFunctionality
{
    public int FunctionalityID { get; set; }
    /// <summary>
    /// references Product
    /// </summary>
    public int ProductID { get; set; }
    public string FunctionalityName { get; set;} = default!;
    public string FunctionalityDescription { get; set; } = default!;
}
