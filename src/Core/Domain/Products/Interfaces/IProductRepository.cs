﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventarioBackend.src.Core.Domain.Products.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(Guid id);
        Task<List<Product>> GetByEntitiAsync(Guid id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task<bool> DeleteAsync(Guid product);
        Task<List<Product>> SearchByNameAsync(string name, Guid entitiId);
        Task<Product?> GetByBarCodeAsync(string barCode, Guid entitiId);
    }
}
