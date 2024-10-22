using MediatR;

namespace Sloth.Application.Services.UIElements;
public class ListWebApplicationIDsQuery: IRequest<IEnumerable<string>?>;
