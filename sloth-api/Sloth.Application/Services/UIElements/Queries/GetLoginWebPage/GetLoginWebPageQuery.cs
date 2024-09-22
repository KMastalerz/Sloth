using MediatR;
using Sloth.Application.DTO;
using Sloth.Domain.Constants;

namespace Sloth.Application.Services.UIElements;
public class GetLoginWebPageQuery: IRequest<GetWebPage>
{
    public string PageID { get; set; } = WebPages.LoginPage;
}
