namespace sloth.Domain.Entities;
public class JobComment
{

    public int CommentID { get; set; } 
    public int JobID { get; set; }
    public string CommentText { get; set; } = default!;
    public Guid CommentedByID { get; set; }
    public bool IsEdited { get; set; } = false;
    public int? OriginalCommentID { get; set; } = null; 

    /// <summary>
    /// External properties
    /// </summary>

    public User CommentedBy { get; set; } = default!;
    public List<JobComment>? PreviousEdits { get; set; } = null;
}
