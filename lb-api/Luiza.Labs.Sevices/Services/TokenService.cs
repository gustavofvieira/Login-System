using Luiza.Labs.Domain.Interfaces.Services.Auth;
using Luiza.Labs.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Luiza.Labs.Sevices.Services
{
    public class TokenService : ITokenService
    {
        private readonly Settings _settings;

        public TokenService(Settings settings)
        {
            _settings = settings;
        }
        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("6e7b2ce2952496d9a8968259e8c2a3d4");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                }),

                Expires = DateTime.UtcNow.AddHours(2),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
