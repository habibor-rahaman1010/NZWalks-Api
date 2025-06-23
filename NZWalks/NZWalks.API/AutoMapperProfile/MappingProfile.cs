using AutoMapper;
using NZWalks.API.DomainEntities;
using NZWalks.API.Dtos.DifficultiesDto;
using NZWalks.API.Dtos.RegionsDto;
using NZWalks.API.Dtos.WalksDto;

namespace NZWalks.API.AutoMapperProfile
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<RegionAddDto, Region>().ReverseMap();
            
            CreateMap<Region, RegionDto>()
                .ForMember(dest => dest.CreatedDate,
                           opt => opt.MapFrom(src => src.CreatedDate.ToString("dd MMM yyyy hh:mm:ss tt")))

                .ForMember(dest => dest.ModifiedDate,
                           opt => opt.MapFrom(src => src.ModifiedDate.HasValue
                                ? src.ModifiedDate.Value.ToString("dd MMM yyyy hh:mm:ss tt")
                                : "Not Modified")).ReverseMap();

            CreateMap<RegionUpdateRequestDto, Region>().ReverseMap();

            CreateMap<DiffcultyAddRequestDto, Difficulty>().ReverseMap();

            CreateMap<Difficulty, DifficultyDto>()
                .ForMember(des => des.CreatedDate,
                            opt => opt.MapFrom(src => src.CreatedDate.ToString("dd MMM yyyy hh:mm:ss tt")))

                .ForMember(des => des.ModifiedDate,
                            opt => opt.MapFrom(src => src.ModifiedDate.HasValue
                                ? src.ModifiedDate.Value.ToString("dd MMM yyyy hh:mm:ss tt")
                                : "Not Modified")).ReverseMap();

            CreateMap<DifficultyUpdateRequestDto, Difficulty>().ReverseMap();

            CreateMap<WalkAddRequestDto, Walk>().ReverseMap();

            CreateMap<Walk, WalkDto>()
                .ForMember(des => des.CreatedDate,
                            opt => opt.MapFrom(src => src.CreatedDate.ToString("dd MMM yyyy hh:mm:ss tt")))

                .ForMember(des => des.ModifiedDate,
                            opt => opt.MapFrom(src => src.ModifiedDate.HasValue
                                ? src.ModifiedDate.Value.ToString("dd MMM yyyy hh:mm:ss tt")
                                : "Not Modified")).ReverseMap();

            CreateMap<Walk, ViewWalkDto>()
                .ForMember(des => des.CreatedDate,
                            opt => opt.MapFrom(src => src.CreatedDate.ToString("dd MMM yyyy hh:mm:ss tt")))

                .ForMember(des => des.ModifiedDate,
                            opt => opt.MapFrom(src => src.ModifiedDate.HasValue
                                ? src.ModifiedDate.Value.ToString("dd MMM yyyy hh:mm:ss tt")
                                : "Not Modified")).ReverseMap();

            CreateMap<WalkUpdateRequestDto, Walk>().ReverseMap();
        }
    }
}
