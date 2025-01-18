using MediatR;
using sloth.Application.Models.Jobs;
using sloth.Domain.Models.Jobs;

namespace sloth.Application.Services.Jobs;
public class ListBugsQuery: ListBugFilters, IRequest<ListBugItemResponse> {}
