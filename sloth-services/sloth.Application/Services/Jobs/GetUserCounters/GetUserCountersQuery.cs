using MediatR;
using sloth.Application.Models.Jobs;

namespace sloth.Application.Services.Jobs;
public class GetUserCountersQuery : IRequest<GetUserCountersItem>;
