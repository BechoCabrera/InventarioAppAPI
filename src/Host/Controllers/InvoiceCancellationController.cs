using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using InventarioBackend.src.Core.Application.Billing.DTOs;
using InventarioBackend.src.Core.Application.Billing.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace InventarioBackend.src.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InvoiceCancellationController : ControllerBase
    {
        private readonly IInvoiceCancellationService _invoiceCancellationService;        
        public InvoiceCancellationController(IInvoiceCancellationService invoiceCancellationService)
        {
            _invoiceCancellationService = invoiceCancellationService;
        }

        [HttpPost]
        public async Task<IActionResult> CancelInvoice([FromBody] InvoiceCancellationDto cancellationDto)
        {
            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            if (role == "vaneaf")
            {
                return NotFound("Usted no tiene acceso a esta funcion.");
            }
            var userIdString = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized(new { message = "Usuario no autenticado." });
            }

            Guid userId = Guid.Parse(userIdString);
            var entitiIdClaim = User.Claims.FirstOrDefault(c => c.Type == "entiti_id")?.Value;            
            cancellationDto.EntitiConfigId = Guid.Parse(entitiIdClaim);
            
            var result = await _invoiceCancellationService.AddInvoicesCancelledAsync(cancellationDto, userId);
           
            if (result)
            {
                return Ok(new { message = "Factura anulada correctamente" });
            }
            else
            {
                return BadRequest(new { message = "Error al anular la factura. Verifique el estado de la factura." });
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<InvoiceCancellationDto>>> GetAll()
        {
            var entitiIdClaim = User.Claims.FirstOrDefault(c => c.Type == "entiti_id")?.Value;

            if (string.IsNullOrEmpty(entitiIdClaim))
            {
                return Unauthorized(new { message = "El ID de la entidad no está disponible." });
            }

            var entitiId = Guid.Parse(entitiIdClaim);
            List<InvoiceCancellationDto> data = await _invoiceCancellationService.GetAllAsync(entitiId); // Usar el filtro aquí
            return Ok(data);
        }
    }
}
