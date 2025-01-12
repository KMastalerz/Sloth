namespace sloth.Domain.Entities;
public class Query: Job
{
    public int QueryID { get; set; }
    public DateTime RaisedDate { get; set; }
}
