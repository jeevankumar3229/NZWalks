using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks_Api.Models.Domains;
using NZWalks_Api.Repositories;

namespace NZWalks_Api.Controllers
{
    [ApiController]
    //[Route("Region")]
    //or
    [Route("[Controller]")]
    public class RegionController : Controller
    {
        private readonly IRegionRepository regionrepository;
        private readonly IMapper mapper;

        //So I will create a constructor and here I will inject the Iregionrepository interface.And when I do that ASP.net core is smart enough that it will give me the
        //implementation of the regionrepository class.
        public RegionController(IRegionRepository regionrepository,IMapper mapper)//created mapper to use automapper
        {
            this.regionrepository = regionrepository;
            this.mapper = mapper;
        }
        //The first method which will be the get all regions.
        [HttpGet]
        public IActionResult GetAllRegions()
        {
            var regions = regionrepository.GetAll();
            /*
            //return Regions DTO
            var regionDTOs= new List<Models.DTOs.Region>();
            regions.ToList().ForEach(region =>
            {
                var regionDTO = new Models.DTOs.Region()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    Area = region.Area,
                    Latitude = region.Latitude,
                    Longitude = region.Longitude,
                    Population = region.Population
                };
                regionDTOs.Add(regionDTO);
            });
            */
            //using mapper
            var regionDTOs = mapper.Map<List<Models.DTOs.Region>>(regions);
            return Ok(regionDTOs);
        }
    }
}
