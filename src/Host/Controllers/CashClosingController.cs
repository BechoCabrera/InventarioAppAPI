﻿using InventarioBackend.src.Core.Application.CashClosings.DTOs;
using InventarioBackend.src.Core.Application.CashClosings.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InventarioBackend.src.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CashClosingController : ControllerBase
    {
        private readonly ICashClosingService _cashClosingService;

        public CashClosingController(ICashClosingService cashClosingService)
        {
            _cashClosingService = cashClosingService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCashClosing([FromBody] CashClosingCreateDto cashClosingDto, [FromQuery] Guid userId)
        {
            Guid? userIdClaim = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            var entitiIdClaim = User.Claims.FirstOrDefault(c => c.Type == "entiti_id")?.Value;
            if (string.IsNullOrEmpty(entitiIdClaim)) return Unauthorized();
            cashClosingDto.EntitiId = Guid.Parse(entitiIdClaim);

            
            // Llamar al servicio para crear el cierre de caja
            var createdCashClosing = await _cashClosingService.CreateAsync(cashClosingDto, cashClosingDto.EntitiId, userIdClaim);

            // Retornar el resultado
            return CreatedAtAction(nameof(GetCashClosing), new { id = createdCashClosing.CashClosingId, userId }, createdCashClosing);
        }

        [HttpGet]
        public async Task<IActionResult> GetCashClosings([FromQuery] Guid entitiId)
        {
            Guid? userIdClaim = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            var entitiIdClaim = User.Claims.FirstOrDefault(c => c.Type == "entiti_id")?.Value;

            if (string.IsNullOrEmpty(entitiIdClaim)) return Unauthorized();
            entitiId = Guid.Parse(entitiIdClaim);

            var cashClosings = await _cashClosingService.GetAllAsync(entitiId);
            return Ok(cashClosings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCashClosing(Guid id, [FromQuery] Guid entitiId)
        {
            var cashClosing = await _cashClosingService.GetByIdAsync(id, entitiId);
            if (cashClosing == null)
            {
                return NotFound();
            }
            return Ok(cashClosing);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCashClosing(Guid id, [FromQuery] Guid entitiId)
        {
            await _cashClosingService.DeleteAsync(id, entitiId);
            return NoContent();
        }
    }
}
