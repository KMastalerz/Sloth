namespace sloth.Domain.Entities;
public class JobFunctionalityHistory
{
    public int JobID { get; set; }
    public int? FunctionalityID { get; set; } = null;
    public Guid ChangedByID { get; set; }
    public DateTime ChangedDate { get; set; }
    public string Action { get; set; } = default!;

    /// <summary>
    /// External properties
    /// </summary>
    public User ChangedBy { get; set; } = default!;
    public ProductFunctionality? Functionality { get; set; } = null;
}
