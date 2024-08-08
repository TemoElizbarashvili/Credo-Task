using AutoMapper;
using Credo.API.Modules.User.Models;
using System.Runtime.CompilerServices;

namespace Credo.API.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile() 
    {
        // Users Profile
        CreateMap<Domain.Entities.User, UserDto>();
    }
}
