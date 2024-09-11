namespace Sloth.Domain.Entities;
/// <summary>
/// This table represents all history of job comment, either removed or updated
/// </summary>
public class JobCommentHistory
{
    public int JobCommentID { get; set; }
    /// <summary>
    /// references Job
    /// </summary>
    public int JobID { get; set; }
    /// <summary>
    /// represents sequancial change in comment revision counts
    /// </summary>
    public int RevisionNumber {  get; set; }
    public string PreviousComment { get; set; } = default!;
    /// <summary>
    /// references User
    /// </summary>
    public Guid Author { get; set; }
    public DateTime CommentDate { get; set; }
    /// <summary>
    /// This flag represents if this comment is an update that can be shared with client. 
    /// Otherwise comments will be hidden from them
    /// </summary>
    public bool IsUpdate { get; set; }
    /// <summary>
    /// If comment was removed not updated. 
    /// </summary>
    public bool IsDeleted { get; set; }
}
