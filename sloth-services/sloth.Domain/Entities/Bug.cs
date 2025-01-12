namespace sloth.Domain.Entities;
public class Bug: Job
{
    public int BugID { get; set; } 
    public DateTime RaisedDate { get; set; }
}
