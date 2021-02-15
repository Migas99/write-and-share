using WriteAndShareWebApi.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WriteAndShareWebApi.Utils
{
    public static class JwtHandler
    {
        public static string GenerateJwtToken(User user, String secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public static string GetUsername(ClaimsPrincipal principal)
        {
            foreach (Claim claim in principal.Claims)
            {
                if (claim.Type == ClaimTypes.Name)
                {
                    return claim.Value;
                }
            }

            throw new Exception("User claim was not found.");
        }

        public static string GetUserRole(ClaimsPrincipal principal)
        {
            foreach (Claim claim in principal.Claims)
            {
                if (claim.Type == ClaimTypes.Role)
                {
                    return claim.Value;
                }
            }

            throw new Exception("User claim was not found.");
        }
    }
}
