using MediatR;
using Sloth.Shared.Models;

namespace Sloth.Application.Services.UIElements;
public class SaveFullWebPageCommand(WebPageItem webPage) : IRequest<string?>
{
    public WebPageItem WebPage { get; set; } = webPage;
}
