using InventarioBackend.Core.Application.Billing.DTOs;
using InventarioBackend.src.Core.Application.Billing.DTOs;
using InventarioBackend.src.Core.Application.Billing.Interfaces;
using InventarioBackend.src.Core.Application.Billing.Services;
using InventarioBackend.src.Core.Application.Products.DTOs;
using InventarioBackend.src.Core.Domain.Security.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InventarioBackend.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoicesController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public async Task<ActionResult<List<InvoiceDto>>> GetAll()
        {
            var entitiIdClaim = User.Claims.FirstOrDefault(c => c.Type == "entiti_id")?.Value;
            var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            if (role == "ADMIN")
            {
                return Ok(await _invoiceService.GetAllAsync());
            }
            else
            {
                if (string.IsNullOrEmpty(entitiIdClaim)) return Unauthorized();

                var entitiId = Guid.Parse(entitiIdClaim);
                var categorias = await _invoiceService.GetByEntitiAsync(entitiId);
                return Ok(categorias);
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var invoice = await _invoiceService.GetByIdAsync(id);
            if (invoice == null)
                return NotFound();

            return Ok(invoice);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InvoiceCreateDto dto)
        {
            var entitiIdClaim = User.Claims.FirstOrDefault(c => c.Type == "entiti_id")?.Value;
            if (string.IsNullOrEmpty(entitiIdClaim)) return Unauthorized();

            dto.EntitiId = Guid.Parse(entitiIdClaim);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _invoiceService.AddAsync(dto);
            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] InvoiceCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _invoiceService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _invoiceService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("byDate")]
        public async Task<IActionResult> GetInvoicesByDate(DateTime date)
        {
            var entitiIdClaim = User.Claims.FirstOrDefault(c => c.Type == "entiti_id")?.Value;
            if (string.IsNullOrEmpty(entitiIdClaim)) return Unauthorized();

            Guid entitiId = Guid.Parse(entitiIdClaim);

            var invoices = await _invoiceService.GetInvoicesByDateAsync(date, entitiId);
            return Ok(invoices);
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<InvoiceDto>>> SearchInvoice([FromQuery] string number)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                return BadRequest("El número de factura no puede estar vacío.");
            }

            // Usa el servicio para obtener las facturas
            var invoices = await _invoiceService.GetInvoicesByNumberAsync(number);

            return Ok(invoices);
        }


        [HttpPost("filter")]
        public async Task<ActionResult<List<InvoiceDto>>> SearchFilter([FromBody] SearchInvoiceRequest request)
        {
            var entitiIdClaim = User.Claims.FirstOrDefault(c => c.Type == "entiti_id")?.Value;
            if (string.IsNullOrEmpty(entitiIdClaim))
                return Unauthorized();

            Guid entitiId = Guid.Parse(entitiIdClaim);

            var invoice = await _invoiceService.GetInvoicesByFiltersAsync(request, entitiId);
            return Ok(invoice);
        }


    }
}

