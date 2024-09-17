namespace Sloth.Application.DTO.UIElements;
/// <summary>
/// Object returned when page is visited
/// </summary>
public class GetWebPageDTO
{
    public string PageID { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public bool CanAccess { get; set; }
    public bool CanAdd { get; set; }
    public bool CanDelete { get; set; }
    ICollection<GetWebPageControlDTO> Controls { get; set; } = [];
}
