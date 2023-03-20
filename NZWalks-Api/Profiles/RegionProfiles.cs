using AutoMapper;
using System.ComponentModel;
using static Azure.Core.HttpHeader;

namespace NZWalks_Api.Profiles
{
    public class RegionProfiles:Profile
    {
        public RegionProfiles()
        {
            //We have to inject these profiles into our solution.
            //inside the constructor we will create maps for our models.
            CreateMap<Models.Domains.Region, Models.DTOs.Region>();//And this way when we specified this automobile is smart enough to look at both the properties and look at
                                                                   //the names of those properties and map the data from one domain model to the other, from the source  to the
                      //destination.
         /*
            //And if the names are not similar, we have to define a map for it to understand, to make sure automapper  understands this is the mapping for us.
             //So if the names were not correct or not matching between the two models, we would have specified it like this.
            CreateMap<Models.Domains.Region, Models.DTOs.Region>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id));
         */
         //can also perform reverseMap
        }
    }
}
