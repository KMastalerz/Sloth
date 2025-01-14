using AutoMapper;
using sloth.Application.Services.Jobs;
using sloth.Domain.Entities;

namespace sloth.Application.Profiles;
public class BugProfile : Profile
{
    public BugProfile()
    {
        CreateMap<CreateQuickJobCommand, Bug>();
    }
}
