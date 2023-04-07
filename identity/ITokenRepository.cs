using Microsoft.AspNetCore.Identity;

namespace identity
{
    public interface ITokenRepository
    {
        public string CreateToken(IdentityUser identityUser, List<string> roles);
    }
}
