using MediatR;
using sloth.Application.Models.Jobs;

namespace sloth.Application.Services.Jobs;
public class ListProductsWithClientIDCommand(Guid? clientID) : IRequest<ListProductsWithClientIDItem>
{
    public Guid? ClientID { get; set; } = clientID;
}
