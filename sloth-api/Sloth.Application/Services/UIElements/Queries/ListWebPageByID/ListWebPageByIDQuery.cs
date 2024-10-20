using MediatR;
using Sloth.Application.DTO;

namespace Sloth.Application.Services.UIElements;
public class ListWebPageByIDQuery(string? appID, string? pageID) : IRequest<IEnumerable<DesignerListWebPageItem>?>
{
    public string? AppID { get; set; } = appID;
    public string? PageID { get; set; } = pageID;
}
