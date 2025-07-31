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
            // 🔐 Step 1: Convert your secret key (string) to a byte array using ASCII encoding.
            var key = Encoding.ASCII.GetBytes(_key);

            // 🎟️ Step 2: Create an instance of JwtSecurityTokenHandler to handle token creation.
            var tokenHandler = new JwtSecurityTokenHandler();

            // 🧾 Step 3: Define the token descriptor, which includes claims, expiration, and signing credentials.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // 🎭 Subject (claims) represents the identity associated with this token.
                Subject = new ClaimsIdentity(new[]
                {
            // 👉 Add custom claims. This example includes the username and user ID.
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            // 🛠️ You can add more claims here, like roles or email.
        }),

                // ⏰ Set token expiration time (here, it's 1 hour from now).
                Expires = DateTime.UtcNow.AddHours(24),

                // ✍️ Add signing credentials using the secret key and HMAC-SHA256 algorithm.
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            // 🪙 Step 4: Create the token based on the descriptor.
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // 📨 Step 5: Serialize the token to a string and return it.
            return tokenHandler.WriteToken(token);
        }


    }
}

