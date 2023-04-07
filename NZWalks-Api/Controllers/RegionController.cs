using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks_Api.CustomActionFilters;
using NZWalks_Api.Models.Domains;
using NZWalks_Api.Models.DTOs;
using NZWalks_Api.Repositories;
using System;
using System.Runtime.Intrinsics.X86;

namespace NZWalks_Api.Controllers
{
    [ApiController]//So the API controller attribute will tell this application that this controller is for API use.
    //[Route("Region")]
    //or
    [Route("[Controller]")]
    //So we will make use of the authorize attribute to block unauthenticated users.
    //So as an attribute, I will say authorize and this comes from Microsoft.ASP.NETCore.authorization
   //[Authorize]
    public class RegionController : Controller
    {
        private readonly IRegionRepository regionrepository;
         private readonly IMapper mapper;

         //So I will create a constructor and here I will inject the Iregionrepository interface.And when I do that ASP.net core is smart enough that it will give me the
         //implementation of the regionrepository class.
         public RegionController(IRegionRepository regionrepository, IMapper mapper)//created mapper to use automapper
         {
             this.regionrepository = regionrepository;
             this.mapper = mapper;
         }
         //The first method which will be the get all regions.
         [HttpGet]
        /*public IActionResult GetAllRegions()
        {
            var regions = regionrepository.GetAll();
            /*
            //return Regions DTO
            var regionDTOs= new List<Models.DTOs.Region>();
            foreach(var region in regions)
            {
            regionDTOs.Add(new Models.DTOs.Region()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageURL=region.RegionImageURL;
                });

            }

            //using mapper
            var regionDTOs = mapper.Map<List<Models.DTOs.Region>>(regions);
            return Ok(regionDTOs);
        }*/
        //to asynchronous
      [Authorize(Roles ="Reader,Writer")]
       
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            var regions = await regionrepository.GetAllAsync();
            //using mapper
            var regionDTOs = mapper.Map<List<Models.DTOs.Region>>(regions);
            return Ok(regionDTOs);
        }


        //get Region by ID
        [HttpGet]
        [Route("{Id:Guid}")]//only allows to take guid value
        [ActionName("GetRegionAsync")]//used for createdataction
        [Authorize(Roles = "Reader,Writer")]
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
        //[ValidateModels]//add this to perform explicit validation using custom action filter
       [Authorize(Roles = "Writer")]
        public async Task<IActionResult> AddRegionAsync(AddRegionRequest addnewregion)
        {
            //So now that we have defined the validations that we want on these properties of this model, we can come back to the region controller where we have the
            //create method and we want to check for these validations .so we can use the ""modelstate.isvalid"" property.
            /*if (ModelState.IsValid)   implicit validation*/
            //{

                //convert addnewregion(DTO) to domain model Region
                var region = new Models.Domains.Region
                {
                    Code = addnewregion.Code,
                    Name = addnewregion.Name,
                    RegionImageURL = addnewregion.RegionImageURL

                };

                //pass details to Repository

                var regions = await regionrepository.AddRegionAsync(region);
                //convert back to DTO
                var regionDTOs = new Models.DTOs.Region
                {
                    Id = regions.Id,
                    Code = regions.Code,
                    Name = regions.Name,
                    RegionImageURL = regions.RegionImageURL

                };

                //as part of the HTTP post create method, because this is creating a resource, we don't usually pass the the ok object back.Instead we pass a createdataction back.
                //So this this gives an HTTP 201 status back to the client.Which means that looking at the 201, the client knows that the the save was successful and the new
                //resource has been created in the database.So I will use the createdataction method and it needs an action name where this resource could be found.
                return CreatedAtAction(nameof(GetRegionAsync)/*same as "GetRegionAsync"*/, new { id = regionDTOs.Id }, regionDTOs);
                //action name    parameter for that action name       response
            /*  implicit validation}
            else
            {
                return BadRequest(ModelState);
            }*/
        }


        //to delete a region
        [HttpDelete]
        [Route("{id:Guid}")]
       [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            //get region from database
            var region = await regionrepository.DeleteRegionAsync(id);
            //if null NotFound
            if (region == null)
            {
                return NotFound();
            }
            //convert response back to DTO
            var regionDTOs = mapper.Map<Models.DTOs.Region>(region);
            //return Ok response
            return Ok(regionDTOs);
        }

        //to update a region
        [HttpPut]
        [ValidateModels]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]

        //if we want to provide all the roles we can provide by specifying this way
        //[Authorize(Roles="Writer,Reader")]
        //or
        //[Authorize]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] UpdateRegionRequest updateregion)//to make clear id is coming from route and model 
        {
            //if (ModelState.IsValid)
            //{
                //data is coming grom body
                //convert updateregion(DTO) to domain model Region
                var region = new Models.Domains.Region
                {
                    Code = updateregion.Code,
                    Name = updateregion.Name,
                    RegionImageURL = updateregion.RegionImageURL,

                };

                //update region using Repository

                var regions = await regionrepository.UpdateRegionAsync(id, region);

                //if null Not Found
                if (regions == null)
                {
                    return NotFound();
                }
                //convert back to DTO
                var regionDTOs = new Models.DTOs.Region
                {
                    Id = regions.Id,
                    Code = regions.Code,
                    Name = regions.Name,
                    RegionImageURL = regions.RegionImageURL,

                };
                // return ok response
                return Ok(regionDTOs);
            /*}
            else
            {
                return BadRequest(ModelState);
            }*/
        }



    }
}
