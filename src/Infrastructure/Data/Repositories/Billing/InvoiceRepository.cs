﻿using InventarioBackend.Core.Application.Billing.DTOs;
using InventarioBackend.Core.Domain.Billing;
using InventarioBackend.Core.Domain.Billing.Interfaces;
using InventarioBackend.src.Core.Domain.Billing.Entities;
using InventarioBackend.src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventarioBackend.Infrastructure.Data.Repositories.Billing
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Invoice> _dbSet;
        public InvoiceRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Invoice>();
        }

        public async Task<List<Invoice>> GetAllAsync()
        {
            return await _dbSet
                .Include(i => i.Client)
                .Include(i => i.Details).ThenInclude(d => d.Product).Include(i => i.EntitiConfigs)
                .ToListAsync();
        }

        public async Task<List<Invoice>> GetByEntitiAsync(Guid id)
        {
            return await _dbSet.Where(a => a.EntitiId == id)
                .Include(i => i.Client)
                .Include(i => i.EntitiConfigs)
                .Include(i => i.Details).ThenInclude(d => d.Product).OrderByDescending(a => a.DueDate)
                .ToListAsync();
        }

        public async Task<Invoice?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .Include(i => i.Client)
                .Include(i => i.Details).ThenInclude(d => d.Product).Include(i => i.EntitiConfigs)
                .FirstOrDefaultAsync(i => i.InvoiceId == id);
        }

        public async Task<Invoice> AddAsync(Invoice invoice)
        {
            try
            {
                await _dbSet.AddAsync(invoice);
                await _context.SaveChangesAsync();
                await _context.Entry(invoice).Reference(i => i.Client).LoadAsync();
                return invoice;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateAsync(Invoice invoice)
        {
            _dbSet.Update(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var invoice = await _dbSet.FindAsync(id);
            if (invoice != null)
            {
                _dbSet.Remove(invoice);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Invoice>> GetInvoicesByDateAsync(DateTime date, Guid entitiId)
        {
            return await _context.Invoices
                .Where(invoice => invoice.CreatedAt.Date == date.Date && invoice.EntitiId == entitiId)
                .ToListAsync();
        }

        public async Task<List<Invoice>> GetInvoicesByNumberAsync(string number)
        {
            return await _context.Invoices
                         .Where(i => i.InvoiceNumber.StartsWith(number))
                         .Include(i => i.Client)
                         .Include(i => i.Details).ThenInclude(d => d.Product).Include(i => i.EntitiConfigs)// Filtra por el prefijo del número de factura
                         .ToListAsync();
        }
    }
}
