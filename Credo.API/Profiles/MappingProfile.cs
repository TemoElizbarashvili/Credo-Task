using AutoMapper;
using Credo.API.Modules.User.Models;
using Credo.API.Modules.Auth.Models;
using Credo.API.Modules.LoanApplications.Models;
using Credo.Application.Modules.LoanApplication.Commands;
using Credo.Common.Models;
using Credo.Domain.Entities;

namespace Credo.API.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile() 
    {
        // Users profile
        CreateMap<User, UserDto>();
        CreateMap<UserRegisterDto, User>();
        CreateMap<PagedList<User>, PagedList<UserDto>>();
        CreateMap<EditUserDto, User>();
        CreateMap<User, UserForApplicationQueryDto>();

        // Loan applications profile
        CreateMap<CreateLoanApplicationDto, LoanApplication>();
        CreateMap<LoanApplication, LoanApplicationQueryDto>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User)); ;
        CreateMap<CreateLoanApplicationCommand, LoanApplication>();
        CreateMap<LoanApplication, CreateLoanApplicationCommand>();
        CreateMap<LoanApplicationListItemDto, LoanApplicationCreated>();
        CreateMap<LoanApplicationCreated, LoanApplicationListItemDto>();
    }
}
