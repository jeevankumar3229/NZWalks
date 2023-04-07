using AutoMapper;

namespace NZAPI.Profiles
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Models.Domains.Region, Models.DTOs.Region>()
                .ReverseMap();

            CreateMap<Models.Domains.Walk, Models.DTOs.Walk>()
                .ReverseMap();

            

            CreateMap<Models.Domains.WalkDifficulty, Models.DTOs.WalkDifficulty>();
        }
    }
}
