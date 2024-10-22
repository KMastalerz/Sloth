using MediatR;
using Sloth.Shared.Models;

namespace Sloth.Application.Services.UIElements;
public class ListWebPageByIDQuery(string? appID, string? pageID) : IRequest<IEnumerable<ListWebPageItem>?>
{
    public string? AppID { get; set; } = appID;
    public string? PageID { get; set; } = pageID;
}
