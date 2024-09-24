namespace Sloth.Application.DTO;
public class GetWebPage
{
    public string PageID { get; set; } = default!;
    public string Title { get; set; } = default!;
    public List<GetWebPanel> WebPanels { get; set; } = [];
}
