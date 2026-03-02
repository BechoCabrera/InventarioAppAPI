using InventarioBackend.src.Core.Application.Promotions.DTOs;
using InventarioBackend.src.Core.Application.Promotions.Interfaces;
using InventarioBackend.src.Core.Domain.Promotions.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InventarioBackend.src.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PromotionsController : ControllerBase
    {
        private readonly IPromotionService _promotionService;

        public PromotionsController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        // 🔹 1. Obtener promociones por entidad
        [HttpGet]
        public async Task<List<PromotionDto>> GetAll()
        {
            var entitiIdClaim = User.Claims
                .FirstOrDefault(c => c.Type == "entiti_id")?.Value;

            if (string.IsNullOrEmpty(entitiIdClaim))
                throw new Exception("Entidad no encontrada.");

            var entitiId = Guid.Parse(entitiIdClaim);

            var promotions = await _promotionService.GetByEntitiAsync(entitiId);

            return promotions;
        }

        // 🔹 2. Crear promoción
        [HttpPost]
        public async Task<IActionResult> Create(PromotionCreateDto dto)
        {
            var entitiIdClaim = User.Claims
                .FirstOrDefault(c => c.Type == "entiti_id")?.Value;

            if (string.IsNullOrEmpty(entitiIdClaim))
                return Unauthorized();

            try
            {
                var id = await _promotionService.CreateAsync(dto);

                return CreatedAtAction(nameof(GetAll),
                    new { id },
                    new { message = "Promoción creada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // 🔹 3. Activar / Desactivar promoción
        [HttpPut("{id}/toggle")]
        public async Task<IActionResult> ToggleStatus(Guid id)
        {
            try
            {
                var result = await _promotionService.ToggleStatusAsync(id);
                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // 🔹 4. Eliminar promoción
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _promotionService.DeleteAsync(id);
                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // 🔹 5. Calcular descuento (USADO EN FACTURA)
        [HttpPost("calculate")]
        public async Task<IActionResult> Calculate(
            [FromBody] List<CartItemDto> items)
        {
            var entitiIdClaim = User.Claims
                .FirstOrDefault(c => c.Type == "entiti_id")?.Value;

            if (string.IsNullOrEmpty(entitiIdClaim))
                return Unauthorized();

            var entitiId = Guid.Parse(entitiIdClaim);

            try
            {
                var result = await _promotionService
                    .CalculateAsync(items, entitiId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PromotionDto>> GetById(Guid id)
        {
            var entitiIdClaim = User.Claims.FirstOrDefault(c => c.Type == "entiti_id")?.Value;
            if (string.IsNullOrEmpty(entitiIdClaim))
                return Unauthorized();

            var entitiId = Guid.Parse(entitiIdClaim);

            // Debes tener un método en el servicio que obtenga la promoción por Id y entidad
            var promotions = await _promotionService.GetByEntitiAsync(entitiId);
            var promotion = promotions.FirstOrDefault(p => p.PromotionId == id);

            if (promotion == null)
                return NotFound();

            return Ok(promotion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, PromotionDto dto)
        {
            if (id == null)
                return BadRequest("El ID de la promoción no coincide.");

            dto.PromotionId = id;
            var entitiIdClaim = User.Claims.FirstOrDefault(c => c.Type == "entiti_id")?.Value;
            if (string.IsNullOrEmpty(entitiIdClaim))
                return Unauthorized();

            try
            {
                // Debes tener un método UpdateAsync en el servicio
                var result = await _promotionService.UpdateAsync(dto, Guid.Parse(entitiIdClaim));
                if (!result)
                    return NotFound("Promoción no encontrada.");

                return Ok(new { message = "Promoción actualizada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}