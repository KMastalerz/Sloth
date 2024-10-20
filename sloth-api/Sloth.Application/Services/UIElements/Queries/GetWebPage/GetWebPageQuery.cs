using MediatR;
using Sloth.Application.DTO;

namespace Sloth.Application.Services.UIElements;
public class GetWebPageQuery(string appID, string pageID) : IRequest<GetWebPage>
{
    public string AppID { get; set; } = appID;
    public string PageID { get; set; } = pageID;
    // TODO: Implement security
    public bool BypassSecurity { get; set; } = true;// WebSecurity.PublicPages.Contains(pageID);
}
