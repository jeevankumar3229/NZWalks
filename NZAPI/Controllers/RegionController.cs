using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZAPI.CustomActionFilters;
using NZAPI.Models.DTOs;
using NZAPI.Repository;
using System.Data;

namespace NZAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class RegionController : Controller
    {
        private readonly IRegionRepository regionrepository;
        private readonly IMapper mapper;

        public RegionController(IRegionRepository regionrepository, IMapper mapper)//created mapper to use automapper
        {
            this.regionrepository = regionrepository;
            this.mapper = mapper;
        }
        
        [HttpGet]

        public async Task<IActionResult> GetAllRegionsAsync()
        {
            var regions = await regionrepository.GetAllAsync();
            //using mapper
            var regionDTOs = mapper.Map<List<Models.DTOs.Region>>(regions);
            return Ok(regionDTOs);
        }


        //get Region by ID
        [HttpGet]
        [Route("{Id:Guid}")]
        [ActionName("GetRegionAsync")]
   
        public async Task<IActionResult> GetRegionAsync(Guid Id)
        {
            var regions = await regionrepository.GetAsync(Id);
            if (regions == null)
            {
                return NotFound();
            }
            var regionDTOs = mapper.Map<Models.DTOs.Region>(regions);
            return Ok(regionDTOs);

        }

        //to add new Region
        [HttpPost]
        [ValidateModels]
        public async Task<IActionResult> AddRegionAsync(AddRegionRequest addnewregion)
        {
            
            var region = new Models.Domains.Region
            {
                Code = addnewregion.Code,
                Name = addnewregion.Name,
                RegionImageURL = addnewregion.RegionImageURL

            };

            

            var regions = await regionrepository.AddRegionAsync(region);
           
            var regionDTOs = new Models.DTOs.Region
            {
                Id = regions.Id,
                Code = regions.Code,
                Name = regions.Name,
                RegionImageURL = regions.RegionImageURL

            };

            
            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTOs.Id }, regionDTOs);
        }


        //to delete a region
        [HttpDelete]
        [Route("{id:Guid}")]
       
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
           
            var region = await regionrepository.DeleteRegionAsync(id);
           
            if (region == null)
            {
                return NotFound();
            }
          
            var regionDTOs = mapper.Map<Models.DTOs.Region>(region);
           
            return Ok(regionDTOs);
        }

        //to update a region
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModels]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] UpdateRegionRequest updateregion)
        {
            
            var region = new Models.Domains.Region
            {
                Code = updateregion.Code,
                Name = updateregion.Name,
                RegionImageURL = updateregion.RegionImageURL,

            };

    

            var regions = await regionrepository.UpdateRegionAsync(id, region);

         
            if (regions == null)
            {
                return NotFound();
            }
         
            var regionDTOs = new Models.DTOs.Region
            {
                Id = regions.Id,
                Code = regions.Code,
                Name = regions.Name,
                RegionImageURL = regions.RegionImageURL,

            };
          
            return Ok(regionDTOs);
            
        }



    }
}
