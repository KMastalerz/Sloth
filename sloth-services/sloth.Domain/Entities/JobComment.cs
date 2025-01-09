namespace sloth.Domain.Entities;
public class JobComment
{

    public int CommentID { get; set; }   // PK
    public int JobID { get; set; } // FK
    public string CommentText { get; set; } = default!;
    public Guid CommentedByID { get; set; } // FK
    // If comment is edited, then it's not displayed, to know the original comment text new comment will have OriginalCommentID which will say which comment is to be displayed in history. 
    public bool IsEdited { get; set; } = false;
    public int? OriginalCommentID { get; set; } = null; // FK (itself)

    /// <summary>
    /// Foreign properties
    /// </summary>

    // FK: CommentedByID
    public User CommentedBy { get; set; } = default!;
    // Refresnce itself, if it has FK: OriginalCommentID then take those.
    public List<JobComment>? PreviousEdits { get; set; } = null;
}
