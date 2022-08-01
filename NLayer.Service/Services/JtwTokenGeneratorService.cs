using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class JtwTokenGeneratorService
    {
        public string GenerateToken(string role)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Batuhanbatuhan.."));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, role));

            JwtSecurityToken token = new JwtSecurityToken(issuer: "https://localhost",claims: claims, audience: "https://localhost", 
                notBefore: DateTime.Now, expires: DateTime.Now.AddMinutes(2), signingCredentials: credentials);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(token);
        }
    }
}
