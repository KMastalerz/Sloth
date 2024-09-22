using MediatR;
using Sloth.Application.DTO;

namespace Sloth.Application.Services.UIElements;
public class GetWebPageQuery(string pageID) : IRequest<GetWebPage>
{
    public string PageID { get; set; } = pageID;
}
