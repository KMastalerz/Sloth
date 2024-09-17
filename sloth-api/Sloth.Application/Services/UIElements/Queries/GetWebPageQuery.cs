using MediatR;
using Sloth.Domain.Entities;

namespace Sloth.Application.Services.UIElements;
public class GetWebPageQuery : IRequest<WebPage?>
{
    public string PageID { get; set; } = default!;
}
