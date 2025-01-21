using MediatR;
using sloth.Application.Models.Jobs;

namespace sloth.Application.Services.Jobs;
public class ListProductsWithClientIDQuery(Guid? clientID) : IRequest<IEnumerable<CacheProductItem>>
{
    public Guid? ClientID { get; set; } = clientID;
}
