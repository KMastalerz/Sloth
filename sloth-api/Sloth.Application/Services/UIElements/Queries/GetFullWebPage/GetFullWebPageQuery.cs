using MediatR;
using Sloth.Shared.Models;

namespace Sloth.Application.Services.UIElements;
public class GetFullWebPageQuery(string appID, string pageID) : IRequest<WebPageItem>
{
    public string AppID { get; set; } = appID;
    public string PageID { get; set; } = pageID;
}
