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
    public class WalkDifficultyController : Controller
    {
        private readonly IwalkDifficultyRepository iwalkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultyController(IwalkDifficultyRepository iwalkDifficultyRepository, IMapper mapper)
        {
            this.iwalkDifficultyRepository = iwalkDifficultyRepository;
            this.mapper = mapper;
        }
        [HttpGet]

        public async Task<IActionResult> GetAllWalkDiffAsync()
        {
            var walkDiff = await iwalkDifficultyRepository.GetWalkDiffAsync();
            var walkDiffDTO = mapper.Map<List<Models.DTOs.WalkDifficulty>>(walkDiff);

            return Ok(walkDiffDTO);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName("walkDifficulty")]
     
        public async Task<IActionResult> GetWalkDiffById(Guid id)
        {
            var walkDiff = await iwalkDifficultyRepository.GetWalkDiffById(id);
            if (walkDiff == null)
            {
                return NotFound();
            }
            var walkDiffDTO = mapper.Map<Models.DTOs.WalkDifficulty>(walkDiff);
            return Ok(walkDiffDTO);
        }

        [HttpPost]
        [ValidateModels] //used for validation using customactionfilters 
       
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
       
        public async Task<IActionResult> UpdateWalkDiff([FromRoute] Guid id, [FromBody] UpdateWalkDiffRequest updateWalkDiffRequest)
        {
            var walkDiffModel = new Models.Domains.WalkDifficulty
            {
                Name = updateWalkDiffRequest.Name
            };
            walkDiffModel = await iwalkDifficultyRepository.UpdateWalkDiffAsync(id, walkDiffModel);
            if (walkDiffModel == null)
            {
                return NotFound();
            }
            var walkDiffDTO = mapper.Map<Models.DTOs.WalkDifficulty>(walkDiffModel);
            return Ok(walkDiffDTO);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
       
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
