namespace Sloth.Domain.Entities;
public class BugComment
{
    public int CommentID { get; set; }
    public int BugID { get; set; }
    public string? Comment { get; set; }
    public DateTime CommentDate { get; set; }
    public Guid CommentBy { get; set; }
}
