using MediatR;
using Sloth.Application.DTO;
using Sloth.Domain.Constants;

namespace Sloth.Application.Services.UIElements;
public class GetWebPageQuery(string pageID) : IRequest<GetWebPage>
{
    public string PageID { get; set; } = pageID;
    // TODO: Implement security
    public bool ByPassSecurity { get; set; } = true;// WebSecurity.PublicPages.Contains(pageID);
}
