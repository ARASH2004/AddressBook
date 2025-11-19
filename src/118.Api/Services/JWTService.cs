using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace _118.Api.Services
{
    public class JWTService
    {
        private readonly string _key;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        public JWTService(IConfiguration configuration,JwtSecurityTokenHandler jwtSecurityTokenHandler)
        {
            _key = configuration["jwt:key"];
            this._jwtSecurityTokenHandler = jwtSecurityTokenHandler;
        }
        public string GenerateToken(User user)
        {
       
            var key = Encoding.ASCII.GetBytes(_key);

        
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
        
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
       
        }),

              
                Expires = DateTime.UtcNow.AddHours(24),

             
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }


    }
}

