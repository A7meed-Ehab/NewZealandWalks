using AutoMapper;
using NewZealandWalks.Api.Models.Domain;
using NewZealandWalks.Api.Models.DTO;

namespace NewZealandWalks.Api.Mappings
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
        CreateMap<Region, RegionDTO>().ReverseMap();
        CreateMap<AddRegionRequestDTO, Region>().ReverseMap();
        CreateMap<UpdateRegionRequestDTO, Region>().ReverseMap();
        CreateMap<AddWalkRequestDTO,Walk>().ReverseMap();
        CreateMap<Walk, WalkDto>().ReverseMap();
        CreateMap<Difficulty,DifficultyDto>().ReverseMap(); 
        }
    }
}
