using AutoMapper;

using DataAccessLayers.Models;
using DataAccessLayers.Models.DTOs;

namespace DeviceAPI.Profiles
{
    public class Mapping : Profile
    {
        protected Mapping()
        {
            CreateMap<Device, DeviceDTO>()
                .ForMember(x => x.type, options => options.MapFrom(x => x.type));
        }
    }
}
