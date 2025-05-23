

using InventarioBackend.src.Core.Application.Security.DTOs;
using InventarioBackend.src.Core.Application.Security.Interfaces;
using InventarioBackend.src.Core.Domain.Security.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InventarioBackend.src.Core.Application.Security.Services;

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
            // Crear las reclamaciones (claims)
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(ClaimTypes.NameIdentifier.ToString(), user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email ?? string.Empty)
        };

            // Añadir los roles como claims (Ahora estamos usando role.Name para agregar el nombre del rol)
            foreach (UserRole role in user.UserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Role.RoleName)); // Aquí agregamos solo el nombre del rol
            }

            // Crear la clave para firmar el token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"] ?? throw new Exception("Jwt Secret no configurada")));

            // Configurar las credenciales para la firma
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Establecer la fecha de expiración del token
            var expires = DateTime.Now.AddMinutes(5);

            // Crear el token JWT
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            // Retornar el token generado
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
