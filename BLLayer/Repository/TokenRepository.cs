using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLLayer.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;

        public TokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string CreateToken(IdentityUser identityUser, List<string> roles)
        {
            var claim = new List<Claim>();
            claim.Add(new Claim(ClaimTypes.Email,identityUser.Email));
            foreach(var role in roles)
            {
                claim.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));

            var signingcredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:Issuer"],
                audience: configuration["JWT:Audience"],
                claims:claim,
                signingCredentials:signingcredentials,
                expires:DateTime.Now.AddMinutes(15)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
