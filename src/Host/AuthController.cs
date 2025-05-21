using InventarioBackend.src.Core.Application.Security.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly byte[] _key;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
        // Aquí debes obtener tu clave secreta de manera segura
        _key = Encoding.ASCII.GetBytes("TuClaveSuperSecretaDeAlMenos32Caracteres!");
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // Aquí valida usuario y contraseña (ejemplo simple)
        if (request.Username != "admin" || request.Password != "12345")
        {
            return Unauthorized(new { message = "Usuario incorrecto" });
        }

        // Crear claims para el token
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, request.Username),
            new Claim(ClaimTypes.Role, "Admin") // ejemplo de rol
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return Ok(new { access_token = tokenString });
    }
}
