using LibraryManagementSystems.Domain.Constant;
using LibraryManagementSystems.Domain.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystems.Infrastructure.Helper
{
    public class SecurityHelper
    {
        private readonly IConfiguration _jwtConfig;

        public SecurityHelper(IConfiguration jwtConfig)
        {
            _jwtConfig = jwtConfig;
        }

        public string TokenGenerator(IdentityUser user)
        {
            var secret = _jwtConfig.GetSection("JwtConfig:Secret").Value;
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("id", user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,
                    DateTime.UtcNow.ToString(), ClaimValueTypes.Integer64),
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _jwtConfig.GetSection("JwtConfig:Issuer").Value,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
