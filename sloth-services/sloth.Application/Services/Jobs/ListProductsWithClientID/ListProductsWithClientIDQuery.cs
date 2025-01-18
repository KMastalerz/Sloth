using MediatR;
using sloth.Application.Models.Miscellaneous;

namespace sloth.Application.Services.Jobs;
public class ListProductsWithClientIDQuery(Guid? clientID) : IRequest<IEnumerable<ListItem>>
{
    public Guid? ClientID { get; set; } = clientID;
}
