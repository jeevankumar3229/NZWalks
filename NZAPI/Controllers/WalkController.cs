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
    public class WalkController : Controller
    {
        private readonly IWalkRepository walkrepository;
        private readonly IMapper mapper;
        private readonly IRegionRepository regionRepository;
        private readonly IwalkDifficultyRepository iwalkDifficultyRepository;

        public WalkController(IWalkRepository walkrepository, IMapper mapper, IRegionRepository regionRepository, IwalkDifficultyRepository iwalkDifficultyRepository)
        {
            this.walkrepository = walkrepository;
            this.mapper = mapper;
            this.regionRepository = regionRepository;
            this.iwalkDifficultyRepository = iwalkDifficultyRepository;
        }

        [HttpGet]
       public async Task<IActionResult> GetAllWalksAsync()
        {
            //fetch data from database using repository
            var walk = await walkrepository.GetAllWalksAsync();
            //convert domain walks to DTO walks
            var walks = mapper.Map<List<Models.DTOs.Walk>>(walk);
            //return ok response
            return Ok(walks);
        }

        //to get walk by Id

        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName("GetWalkById")]
        
        public async Task<IActionResult> GetWalkById(Guid id)
        {
            var walk = await walkrepository.GetWalkById(id);
            if (walk == null)
            {
                return NotFound();
            }
            var walks = mapper.Map<Models.DTOs.Walk>(walk);

            return Ok(walks);
        }

        //add a new walk
        [HttpPost]
        [ValidateModels]//action attribute
       
        public async Task<IActionResult> AddWalkAsync([FromBody] AddNewWalk NewWalk)
        {
            
            if (!await validatewalk(NewWalk))
            {
                return BadRequest(ModelState);
            }
            
            var walk = new Models.Domains.Walk
            {
                Name = NewWalk.Name,
                Length = NewWalk.Length,
                Description = NewWalk.Description,
                WalkImageURL = NewWalk.WalkImageURL,
                RegionId = NewWalk.RegionId,
                WalkDifficultyId = NewWalk.WalkDifficultyId
            };

            //pass domain walk to repository to add to database
            var walks = await walkrepository.AddWalkAsync(walk);

            //convert domain to dto
            var walkDTO = new Models.DTOs.Walk
            {
                Id = walks.Id,
                Name = walks.Name,
                Description = walks.Description,
                Length = walks.Length,
                WalkImageURL = walks.WalkImageURL,
                RegionId = walks.RegionId,
                WalkDifficultyId = walks.WalkDifficultyId
            };
          
            return CreatedAtAction("GetWalkById", new { id = walkDTO.Id }, walkDTO);
           
        }

        //update a walk
        [HttpPut]
        [ValidateModels]
        [Route("{id:Guid}")]
        
        public async Task<IActionResult> UpdateWalkAsync(Guid id, UpdateWalk updatewalk)
        {
            if (!await validatewalk(updatewalk))
            {
                return BadRequest(ModelState);
            }
            
            var walk = new Models.Domains.Walk
            {
                Name = updatewalk.Name,
                Length = updatewalk.Length,
                Description = updatewalk.Description,
                WalkImageURL = updatewalk.WalkImageURL,
                RegionId = updatewalk.RegionId,
                WalkDifficultyId = updatewalk.WalkDifficultyId
            };
            //pass to update it
            var walks = await walkrepository.UpdateWalkAsync(id, walk);
            //if null
            if (walks == null)
            {
                return NotFound();
            }
            //map domain to DTO
            var walkDTO = new Models.DTOs.Walk
            {
                Id = walks.Id,
                Name = walks.Name,
                Description = walks.Description,
                WalkImageURL = walks.WalkImageURL,
                Length = walks.Length,
                RegionId = walks.RegionId,
                WalkDifficultyId = walks.WalkDifficultyId
            };
            //send ok response
            return Ok(walkDTO);
            
        }


        //delete a walk
        [HttpDelete]
        [Route("{id:Guid}")]
      
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            var walk = await walkrepository.DeleteWalkAsync(id);
            if (walk == null)
            {
                return NotFound();
            }
            var walks = mapper.Map<Models.DTOs.Walk>(walk);

            return Ok(walks);

        }

        //validating RegionId and WalkDifficultyId in Addwalk 
        private async Task<bool> validatewalk(AddNewWalk NewWalk)
        {
            if (NewWalk == null)
            {
                ModelState.AddModelError(nameof(NewWalk), $"cannot be null");
                return false;
            }
            if (await regionRepository.GetAsync(NewWalk.RegionId) == null)
            {
                ModelState.AddModelError(nameof(NewWalk.RegionId), $"enter a valid RegionId");

            }
            if (await iwalkDifficultyRepository.GetWalkDiffById(NewWalk.WalkDifficultyId) == null)
            {
                ModelState.AddModelError(nameof(NewWalk.WalkDifficultyId), $"enter a valid WalkDifficultyId");

            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }

        //validating RegionId and WalkDifficultyId in updatewalk 
        private async Task<bool> validatewalk(UpdateWalk updatewalk)
        {
            if (updatewalk == null)
            {
                ModelState.AddModelError(nameof(updatewalk), $"cannot be null");
                return false;
            }
            if (await regionRepository.GetAsync(updatewalk.RegionId) == null)
            {
                ModelState.AddModelError(nameof(updatewalk.RegionId), $"enter a valid RegionId");

            }
            if (await iwalkDifficultyRepository.GetWalkDiffById(updatewalk.WalkDifficultyId) == null)
            {
                ModelState.AddModelError(nameof(updatewalk.WalkDifficultyId), $"enter a valid WalkDifficultyId");

            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }

    }
}
