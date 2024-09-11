namespace Sloth.Domain.Entities;
/// <summary>
/// Table of job related comments in table
/// </summary>
public class JobComment
{
    public int JobCommentID { get; set; } 
    /// <summary>
    /// references Job
    /// </summary>
    public int JobID { get; set; }
    public string Comment { get; set; } = default!;
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
    /// Indicates if comment was edited. 
    /// </summary>
    public bool IsEdited { get; set; }
}
