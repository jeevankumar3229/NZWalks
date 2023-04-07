using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NZWalks_Api.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration configurations;

        public TokenRepository(IConfiguration configurations)//to access appsettings.json
        {
            this.configurations = configurations;
        }
        public string CreateJWTToken(IdentityUser user, List<string> roles)
        {
            
            //create claims
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configurations["JWT:key"]));
            var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(

                configurations["JWT:issuer"],
                configurations["JWT:Audience"],
                claims,
                expires:DateTime.Now.AddMinutes(15),
                signingCredentials:credentials
                );

            //to return token as a string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
