namespace Sloth.Domain.Entities;
/// <summary>
/// This table represents all Bugs within Sloth
/// </summary>
public class Bug
{
    public int BugID { get; set; }
    public string? BugHeader { get; set; }
    public string? BugDescription { get; set; }
    public DateTime DateCreated { get; set; }
}
