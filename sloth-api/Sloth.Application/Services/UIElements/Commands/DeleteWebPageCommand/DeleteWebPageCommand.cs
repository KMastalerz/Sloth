using MediatR;

namespace Sloth.Application.Services.UIElements;
public class DeleteWebPageCommand(string appID, string pageID): IRequest
{
    public string AppID { get; set; } = appID;
    public string PageID { get; set; } = pageID;
}
