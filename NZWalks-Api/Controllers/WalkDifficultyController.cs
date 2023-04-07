using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks_Api.CustomActionFilters;
using NZWalks_Api.Models.DTOs;
using NZWalks_Api.Repositories;
using System.Data;

namespace NZWalks_Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class WalkDifficultyController:Controller
    {
        private readonly IwalkDifficultyRepository iwalkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultyController(IwalkDifficultyRepository iwalkDifficultyRepository,IMapper mapper)
        {
            this.iwalkDifficultyRepository = iwalkDifficultyRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetAllWalkDiffAsync()
        {
            var walkDiff=await iwalkDifficultyRepository.GetWalkDiffAsync();
            var walkDiffDTO = mapper.Map<List<Models.DTOs.WalkDifficulty>>(walkDiff);

            return Ok(walkDiffDTO);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName("walkDifficulty")]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetWalkDiffById(Guid id)
        {
            var walkDiff=await iwalkDifficultyRepository.GetWalkDiffById(id);
            if (walkDiff == null)
            {
                return NotFound();
            }
            var walkDiffDTO = mapper.Map<Models.DTOs.WalkDifficulty>(walkDiff);
            return Ok(walkDiffDTO);
        }

        [HttpPost]
        [ValidateModels] //used for validation using customactionfilters 
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> AddWalkDiffAsync([FromBody] AddWalkDiffRequest walkDiffRequest)
        {
            var walkDiffModel = new Models.Domains.WalkDifficulty
            {
                Name = walkDiffRequest.Name
            };
            walkDiffModel = await iwalkDifficultyRepository.AddWalkDifficultyAsync(walkDiffModel);
            var walkDiffDTO = mapper.Map<Models.DTOs.WalkDifficulty>(walkDiffModel);
            return CreatedAtAction("walkDifficulty", new { id = walkDiffDTO.Id }, walkDiffDTO);

        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModels]
        [Authorize(Roles = "WRITER")]
        public async Task<IActionResult> UpdateWalkDiff([FromRoute] Guid id, [FromBody] UpdateWalkDiffRequest updateWalkDiffRequest)
        {
            var walkDiffModel = new Models.Domains.WalkDifficulty
            {
                Name = updateWalkDiffRequest.Name
            };
            walkDiffModel = await iwalkDifficultyRepository.UpdateWalkDiffAsync(id,walkDiffModel);
            if (walkDiffModel == null)
            {
                return NotFound();
            }
            var walkDiffDTO = mapper.Map<Models.DTOs.WalkDifficulty>(walkDiffModel);
            return Ok(walkDiffDTO);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteWalkDiffAsync(Guid id)
        {
            var walkDiff = await iwalkDifficultyRepository.DeleteWalkDiffAsync(id);
            if (walkDiff == null)
            {
                return NotFound();
            }
            else
            {
                var walkDiffDTO = mapper.Map<Models.DTOs.WalkDifficulty>(walkDiff);
                return Ok(walkDiffDTO);
            }
        }
    }
}
