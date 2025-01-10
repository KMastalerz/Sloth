using MediatR;
using sloth.Application.Models.Job;

namespace sloth.Application.Services.Job;
public class ListJobDataCacheQuery : IRequest<ListJobDataCacheItem>;
