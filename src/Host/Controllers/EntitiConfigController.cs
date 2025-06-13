using InventarioBackend.src.Core.Application.EntitiConfigs.DTOs;
using InventarioBackend.src.Core.Application.EntitiConfigs.Interfaces;
using InventarioBackend.src.Core.Application.EntitiConfigs.Services;
using InventarioBackend.src.Core.Application.Products.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EntitiConfigController : ControllerBase
{
    private readonly IEntitiConfigService _service;

    public EntitiConfigController(IEntitiConfigService service)
    {
        _service = service;
    }

    [HttpGet("{code}")]
    public async Task<ActionResult<EntitiConfigDto>> GetByCode(string code)
    {
        var result = await _service.GetByCodeAsync(code);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<EntitiConfigDto>> Create([FromBody] EntitiConfigCreateDto dto)
    {
        var entitiIdClaim = User.Claims.FirstOrDefault(c => c.Type == "entiti_id")?.Value;
        if (string.IsNullOrEmpty(entitiIdClaim)) return Unauthorized();
        var entitiId = Guid.Parse(entitiIdClaim);

        var result = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetByCode), new { code = result.Code }, result);
    }

    [HttpPut]
    public async Task<ActionResult<string>> Update([FromBody] EntitiConfigUpdateDto dto)
    {
        var result = await _service.UpdateAsync(dto);
        return Ok(new { message = $"{result.EntitiName}" });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var data = await _service.GetAllAsync();
            return Ok(data);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "Error interno del servidor",
                error = ex.Message,
                inner = ex.InnerException?.Message
            });
        }
    }
    

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
       var result = await _service.DeleteAsync(id);
        return Ok(new { message = $"{result}" });
    }

    [HttpGet("myentiti")]
    public async Task<IActionResult> GetMyEntiti()
    {
        var entitiIdClaim = User.Claims.FirstOrDefault(c => c.Type == "entiti_id")?.Value;
        if (string.IsNullOrEmpty(entitiIdClaim))
            return Unauthorized();
        var entitiId = Guid.Parse(entitiIdClaim);

        var entiti = await _service.GetByIdEntitiAsync(entitiId); // tu método habitual

        if (entiti == null)
            return NotFound();

        return Ok(new { data = entiti });
    }
}
