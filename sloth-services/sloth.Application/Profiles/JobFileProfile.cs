using AutoMapper;
using sloth.Application.Models.Jobs;
using sloth.Domain.Entities;

namespace sloth.Application.Profiles;
public class JobFileProfile : Profile
{
    public JobFileProfile()
    {
        CreateMap<JobFile, GetFileBugItem>().ReverseMap();
    }
}
