﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;

namespace identity.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AuthController : Controller
    {
        
        private readonly UserManager<IdentityUser> user;
        private readonly ITokenRepository tokenRepository;
        private readonly ILogger<AuthController> logger;
        private readonly IUserStore<IdentityUser> store;

        public AuthController(UserManager<IdentityUser> user,ITokenRepository tokenRepository, ILogger<AuthController> logger)
        {
            this.user = user;
            this.tokenRepository = tokenRepository;
            this.logger = logger;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUser(Register register)
        {
            //var user = new UserManager<IdentityUser>(store,null,null,null,null,null,null,null,null);
            var Identityuser = new IdentityUser
            {
                UserName = register.Username,
                Email = register.Username
            };
            var result = await user.CreateAsync(Identityuser, register.Password);

            if (result.Succeeded)
            {
                logger.LogInformation("Successfully User Created");
                try
                {
                    var addrolesresult = await user.AddToRolesAsync(Identityuser, register.roles);
                    return Ok("User Successfully Registered");

                }
                catch (Exception e)
                {
                    logger.LogError(e.Message);
                    return BadRequest(e.Message + " " + "Enter \n 1)Read for Read access \n 2)Write for Write access");
                }

            }
            else
            {
                logger.LogError("Error Occurred while creating new User");
                return BadRequest(result);
            }

        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginUser(LoginRequest loginRequest)
        {
            //var user = new UserManager<IdentityUser>(new UserStore<IdentityUser>(new DeviceAuthDbContext<IdentityUser>(new DbC);

            var Identityuser = new IdentityUser
            {
                UserName = loginRequest.Username,
                Email = loginRequest.Username
            };
            var findresult = await user.FindByEmailAsync(loginRequest.Username);
            if (findresult != null)
            {
                logger.LogInformation("User Found");
                var result = await user.CheckPasswordAsync(findresult, loginRequest.Password);
                if (result != false)
                {
                    logger.LogInformation("Successfully logged in");
                    var roles = await user.GetRolesAsync(findresult);
                    var token = tokenRepository.CreateToken(findresult, roles.ToList());
                    return Ok(token);

                }
                else
                {
                    logger.LogError("Invalid password");
                    return BadRequest("Invalid Password");
                }

            }
            else
            {
                var a = 1;
                logger.LogError("User Not Found");
                return BadRequest("Invalid User");
            }

        }
    }
}
