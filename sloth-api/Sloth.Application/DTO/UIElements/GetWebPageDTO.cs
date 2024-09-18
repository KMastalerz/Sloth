namespace Sloth.Application.DTO;
public class GetWebPageDTO
{
    public string PageID { get; set; } = default!;
    public string Title { get; set; } = default!;
    public ICollection<GetWebControlDTO> WebControls { get; set; } = [];
}
