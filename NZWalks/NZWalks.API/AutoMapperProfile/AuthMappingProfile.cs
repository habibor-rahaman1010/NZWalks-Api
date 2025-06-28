using NZWalks.API.Dtos.AuthenticationDto;

namespace NZWalks.API.AutoMapperProfile
{
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            CreateMap<UserRegisterRequestDto, ApplicationUser>().ReverseMap();
        }
    }
}
