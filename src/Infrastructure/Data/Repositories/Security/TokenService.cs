

using InventarioBackend.src.Core.Application.Security.DTOs;
using InventarioBackend.src.Core.Application.Security.Interfaces;
using InventarioBackend.src.Core.Domain.Security.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InventarioBackend.src.Core.Application.Security.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokenResult GenerateToken(User user)
        {
            try
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier.ToString(), user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),

                new Claim(ClaimTypes.Email, user.Email ?? string.Empty)
            };
                foreach (var role in user.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Name)); // Asumiendo que Role tiene propiedad Name
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"] ?? throw new Exception("Jwt Secret no configurada")));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var expires = DateTime.Now.AddMinutes(5);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: expires,
                    signingCredentials: creds
                );
                return new TokenResult
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = expires
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
