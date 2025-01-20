namespace sloth.Domain.Entities;
public class JobFile
{
    public Guid FileID { get; set; }
    public int JobID { get; set; }
    public string Name { get; set; } = default!;
    public long Size { get; set; }
    public string Extension { get; set; } = default!;
    public Guid AddedByID {  get; set; } 
    public DateTime AddedDate { get; set; }

    public User AddedBy { get; set; } = default!;
}
