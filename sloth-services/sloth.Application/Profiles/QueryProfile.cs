using AutoMapper;
using sloth.Application.Services.Jobs;
using sloth.Domain.Entities;

namespace sloth.Application.Profiles;
public class QueryProfile : Profile
{
    public QueryProfile()
    {
        CreateMap<CreateQuickJobCommand, Query>();
    }
}
