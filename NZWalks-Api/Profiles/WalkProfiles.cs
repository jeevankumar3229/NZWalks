using AutoMapper;

namespace NZWalks_Api.Profiles
{
    public class WalkProfiles:Profile
    {
        public WalkProfiles()
        {
            CreateMap<Models.Domains.Region, Models.DTOs.Walk>()
                .ReverseMap();

            //we can create separate profile for WalkDifficulty

            CreateMap<Models.Domains.WalkDifficulty, Models.DTOs.WalkDifficulty>();
        }
    }
}
