using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks_Api.Models.Domains;
using NZWalks_Api.Models.DTOs;
using NZWalks_Api.Repositories;

namespace NZWalks_Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class WalkController:Controller
    {
        private readonly IWalkRepository walkrepository;
        private readonly IMapper mapper;

        public WalkController(IWalkRepository walkrepository,IMapper mapper)
        {
            this.walkrepository = walkrepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            //fetch data from database using repository
            var walk = await walkrepository.GetAllWalksAsync();
            //convert domain walks to DTO walks
            var walks = mapper.Map < List < Walk >> (walk);
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
        public async Task<IActionResult> AddWalkAsync([FromBody] AddNewWalk NewWalk)
        {
            //convert newwalk to domain model walk
            var walk = new Models.Domains.Walk
            {
                Name = NewWalk.Name,
                Length = NewWalk.Length,
                RegionId = NewWalk.RegionId,
                WalkDifficultyId = NewWalk.WalkDifficultyId
            };

            //pass domain walk to repository to add to database
            var walks =await walkrepository.AddWalkAsync(walk);

            //convert domain to dto
            var walkDTO = new Models.DTOs.Walk
            {
                Id=walks.Id,
                Name=walks.Name,
                Length=walks.Length,
                RegionId=walks.RegionId,
                WalkDifficultyId=walks.WalkDifficultyId
            };
            //send response  to client
            return CreatedAtAction("GetWalkById", new { id = walkDTO.Id }, walkDTO);
        }

        //update a walk
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateWalkAsync(Guid id,UpdateWalk updatewalk)
        {
            //convert to domain model walk
            var walk = new Models.Domains.Walk
            {
                Name = updatewalk.Name,
                Length = updatewalk.Length,
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
            var walk = walkrepository.DeleteWalkAsync(id);
            if (walk == null)
            {
                return NotFound();
            }
            var walks = mapper.Map<Models.DTOs.Walk>(walk);

            return Ok(walks);
    
        }
    }
}
