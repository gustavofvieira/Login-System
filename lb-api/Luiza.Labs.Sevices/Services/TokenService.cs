using Luiza.Labs.Domain.Interfaces.Services.Auth;
using Luiza.Labs.Domain.Models;
using Luiza.Labs.Domain.Models.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Luiza.Labs.Sevices.Services
{
    public class TokenService : ITokenService
    {
        //private readonly SettingsOptions _settings;
        //private readonly IConfiguration _configuration;

        //public TokenService(
        //    //IOptions<SettingsOptions> settings,
        //    IConfiguration configuration)
        //{
        //    //_settings = settings.Value;
        //    _configuration = configuration;
        //}
        public Token GenerateToken(User user)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_settings.Secret);
            var key = Encoding.ASCII.GetBytes("6e7b2ce2952496d9a8968259e8c2a3d4");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.Role),
                }),

                Expires = DateTime.UtcNow.AddHours(2),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var jwtKey = new Token { JwtKey = tokenHandler.WriteToken(token) };

            return jwtKey;
        }
    }
}
