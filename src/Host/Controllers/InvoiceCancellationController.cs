using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using InventarioBackend.src.Core.Application.Billing.Services;
using InventarioBackend.src.Core.Application.Billing.DTOs;
using InventarioBackend.src.Core.Application.Billing.Interfaces;
using Microsoft.AspNetCore.Authorization;
using InventarioBackend.Core.Application.Billing.DTOs;
using InventarioBackend.Core.Application.Billing.Services;

namespace InventarioBackend.src.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InvoiceCancellationController : ControllerBase
    {
        private readonly IInvoiceCancellationService _invoiceCancellationService;

        // Inyección de la interfaz IInvoiceCancellationService
        public InvoiceCancellationController(IInvoiceCancellationService invoiceCancellationService)
        {
            _invoiceCancellationService = invoiceCancellationService;
        }

        [HttpPost]
        public async Task<IActionResult> CancelInvoice([FromBody] InvoiceCancellationDto cancellationDto)
        {
            // Obtener el UserId desde los claims
            var userIdString = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized(new { message = "Usuario no autenticado." });
            }

            Guid userId = Guid.Parse(userIdString);

            // Llamar al servicio de anulación
            var result = await _invoiceCancellationService.AddInvoicesCancelledAsync(cancellationDto, userId);

            // Verificar si la factura fue anulada correctamente
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
            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (role == "ADMIN")
            {
                return Ok(await _invoiceCancellationService.GetAllAsync());
            }
            else
            {
                if (string.IsNullOrEmpty(entitiIdClaim)) return Unauthorized();

                var entitiId = Guid.Parse(entitiIdClaim);
                List<InvoiceCancellationDto> data = await _invoiceCancellationService.GetAllAsync();
                return Ok(data);
            }
        }
    }
}
