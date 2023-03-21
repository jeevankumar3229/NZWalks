using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks_Api.Models.Domains;
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
        public async Task<IActionResult> GetWalkById(Guid id)
        {
            var walk = await walkrepository.GetWalkById(id);
            if (walk == null)
            {
                return NotFound();
            }
            var walks = mapper.Map<List<Walk>>(walk);

            return Ok(walks);
        }
    }
}
