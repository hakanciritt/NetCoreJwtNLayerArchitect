using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace SharedLibrary.Services
{
    public static class SignService
    {
        public static SecurityKey GetSymmetricSecurityKey(string securityKey)
        {
            var byteArray = Encoding.UTF8.GetBytes(securityKey);
            return new SymmetricSecurityKey(byteArray);
        }
    }
}
