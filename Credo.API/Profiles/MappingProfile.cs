using AutoMapper;
using Credo.API.Modules.User.Models;
using Credo.API.Modules.Auth.Models;
using Credo.Common.Models;
using Credo.Domain.Entities;

namespace Credo.API.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile() 
    {
        // Users Profile
        CreateMap<User, UserDto>();
        CreateMap<UserRegisterDto, User>();
        CreateMap<PagedList<User>, PagedList<UserDto>>();
        CreateMap<EditUserDto, User>();
    }
}
