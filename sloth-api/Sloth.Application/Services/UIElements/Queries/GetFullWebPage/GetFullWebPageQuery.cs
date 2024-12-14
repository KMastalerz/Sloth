using MediatR;
using Sloth.Shared.DTO;
namespace Sloth.Application.Services.UIElements;
public class GetFullWebPageQuery(string appID, string pageID) : IRequest<GetWebPageFull?>
{
    public string AppID { get; set; } = appID;
    public string PageID { get; set; } = pageID;
}
