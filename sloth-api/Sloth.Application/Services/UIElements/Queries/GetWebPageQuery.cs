using MediatR;
using Sloth.Application.DTO;

namespace Sloth.Application.Services.UIElements;
public class GetWebPageQuery : IRequest<GetWebPageDTO?>
{
    public string PageID { get; set; } = default!;
}
