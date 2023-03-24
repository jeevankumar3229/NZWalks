using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using NZWalks_Api.Models.DTOs;
using NZWalks_Api.Repositories;
using System.Runtime.Intrinsics.X86;

namespace NZWalks_Api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> user;
        private readonly ITokenRepository tokenrepository;

        //we now want to register a user and we use the user manager class.That identity provides us to register a user or to create a user.
        public AuthController(UserManager<IdentityUser>/*takes type of user*/ user,ITokenRepository tokenrepository)
        {
            this.user = user;
            this.tokenrepository = tokenrepository;
        }
        //Post:../Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerrequest)
        {
            var Identityuser = new IdentityUser
            {
                UserName = registerrequest.Username,
                Email = registerrequest.Username
            };
            var identityresult =await user.CreateAsync(Identityuser, registerrequest.Password);
            if (identityresult.Succeeded)
            {
                //add roles to user
                if (registerrequest.Roles != null)
                {
                    identityresult = await user.AddToRolesAsync(Identityuser, registerrequest.Roles);
                    if (identityresult.Succeeded)
                    {
                        return Ok("User was registered!please login");
                    }
                }
            }
            return BadRequest("Something went wrong");
        }
        //Post:../Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginrequest)
        {
           var loginuser= await user.FindByEmailAsync(loginrequest.Username);
            if (loginuser != null)
            {
                var checkpassword=await user.CheckPasswordAsync(loginuser, loginrequest.Password);
                if (checkpassword)
                {
                    //Get roles for the user
                    var roles = await user.GetRolesAsync(loginuser);
                    if (roles != null)
                    {
                        //Create Token
                        var jwttoken=tokenrepository.CreateJWTToken(loginuser, roles.ToList());
                        var response = new LoginResponseDTO
                        {
                            JwtToken = jwttoken
                        };
                        return Ok(response);
                    }
                    
                }
            }
            return BadRequest("Username or password incorrect");
        }
    }
}
