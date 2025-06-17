using InventarioBackend.Core.Application.Clients.Services;
using InventarioBackend.src.Core.Application.Clients.DTOs;
using InventarioBackend.src.Core.Application.Clients.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ClientsController : ControllerBase
{
    private readonly IClientService _service;

    public ClientsController(IClientService service)
    {
        _service = service;
    }

    //[HttpGet]
    //public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ClientCreateDto dto)
    {
        var entitiIdClaim = User.Claims.FirstOrDefault(c => c.Type == "entiti_id")?.Value;
        if (string.IsNullOrEmpty(entitiIdClaim)) return Unauthorized();

        dto.EntitiId = Guid.Parse(entitiIdClaim);
        await _service.AddAsync(dto);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var entitiIdClaim = User.Claims.FirstOrDefault(c => c.Type == "entiti_id")?.Value;
        var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
        if (role == "ADMIN")
        {
            return Ok(await _service.GetAllAsync());
        }
        else
        {
            if (string.IsNullOrEmpty(entitiIdClaim)) return Unauthorized();

            var entitiId = Guid.Parse(entitiIdClaim);

            var result = await _service.GetByEntitiAsync(entitiId);
            return Ok(result);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, ClientUpdateDto clientDto)
    {
        if (id != clientDto.ClientId)
        {
            return BadRequest("El ID del cliente no coincide.");
        }

        try
        {
            var updatedClient = await _service.UpdateAsync(id, clientDto);

            return Ok(new { message = $"{updatedClient}" });

        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);  // Manejo de errores
        }
    }
    // Agrega Update, Delete, GetById igual que en ProductsController
}
