using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLayer.Repository
{
    public interface ITokenRepository
    {
        public string CreateToken(IdentityUser identityUser,List<string> roles);
    }
}
