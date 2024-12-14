using MediatR;
using Sloth.Shared.DTO;

namespace Sloth.Application.Services.UIElements;
public class SaveFullWebPageCommand(GetWebPageFull webPage) : IRequest<GetWebPageFull>
{
    public GetWebPageFull WebPage { get; set; } = webPage;
}
