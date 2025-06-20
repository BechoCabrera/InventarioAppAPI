using InventarioBackend.src.Core.Application.CashClosings.DTOs;
using InventarioBackend.src.Core.Application.CashClosings.Interfaces;
using InventarioBackend.src.Core.Domain.CashClosings.Entities;
using InventarioBackend.src.Infrastructure.Data;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioBackend.src.Core.Application.CashClosings.Services
{
    public class CashClosingService : ICashClosingService
    {
        private readonly AppDbContext _context;

        public CashClosingService(AppDbContext context)
        {
            _context = context;
        }

        // Crear un nuevo cierre de caja
        public async Task<CashClosingDto> CreateAsync(CashClosingCreateDto cashClosingDto, Guid? entitiId, Guid? createdBy)
        {
            var cashClosing = cashClosingDto.Adapt<CashClosing>();
            cashClosing.CashClosingId = Guid.NewGuid();
            cashClosing.CreatedAt = DateTime.Now;
            cashClosing.Date = DateTime.Now;
            cashClosing.CreatedBy = createdBy; // Asignar el usuario que crea el cierre
            cashClosing.EntitiId = entitiId; // Asignar la entidad

            // Calcular el total general
            cashClosing.TotalAmount = cashClosing.TotalCash + cashClosing.TotalCredit + cashClosing.TotalCard + cashClosing.TotalTransfer;

            _context.CashClosings.Add(cashClosing);
            await _context.SaveChangesAsync();

            return cashClosing.Adapt<CashClosingDto>();
        }
        // Obtener todos los cierres de caja filtrados por EntitiId
        public async Task<IEnumerable<CashClosingDto>> GetAllAsync(Guid entitiId)
        {
            var cashClosings = await _context.CashClosings
                                             .Where(c => c.EntitiId == entitiId) // Filtrar por entidad
                                             .ToListAsync();

            return cashClosings.Adapt<IEnumerable<CashClosingDto>>();
        }

        // Obtener un cierre de caja por ID y EntitiId
        public async Task<CashClosingDto> GetByIdAsync(Guid id, Guid entitiId)
        {
            var cashClosing = await _context.CashClosings
                                            .FirstOrDefaultAsync(c => c.CashClosingId == id && c.EntitiId == entitiId);

            return cashClosing?.Adapt<CashClosingDto>();
        }

        // Eliminar un cierre de caja por ID y EntitiId
        public async Task DeleteAsync(Guid id, Guid entitiId)
        {
            var cashClosing = await _context.CashClosings
                                            .FirstOrDefaultAsync(c => c.CashClosingId == id && c.EntitiId == entitiId);

            if (cashClosing != null)
            {
                _context.CashClosings.Remove(cashClosing);
                await _context.SaveChangesAsync();
            }
        }
    }
}
