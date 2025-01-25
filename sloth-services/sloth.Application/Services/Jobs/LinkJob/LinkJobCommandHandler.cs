using MediatR;
using sloth.Application.Models.Jobs;

namespace sloth.Application.Services.Jobs;
public class LinkJobCommandHandler : IRequestHandler<LinkJobCommand, LinkJobItem>
{
    public Task<LinkJobItem> Handle(LinkJobCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
