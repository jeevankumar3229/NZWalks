using logindemo.Data;
using logindemo.Models.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace logindemo.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AuthorizationController : Controller
    {
        private readonly AuthDbContext dbcontext;
        private readonly UserManager<IdentityUser> identity;

        public AuthorizationController(AuthDbContext dbcontext, UserManager<IdentityUser> identity)
        {
            this.dbcontext = dbcontext;
            this.identity = identity;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(Register register)
        {
            var user = new IdentityUser
            {
                UserName = register.Username,
                Email = register.Username
            };
            var result = await identity.CreateAsync(user, register.Password);

            if (result.Succeeded)
            {
                if (register.role != null)
                {

                    foreach (var roles in register.role)
                    {
                        await identity.AddToRoleAsync(user, roles);
                    }
                    return Ok("user created");
                }



            }
            return BadRequest("Error occurred");

        }
    }
}
