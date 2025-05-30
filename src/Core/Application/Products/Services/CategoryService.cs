using InventarioBackend.src.Core.Application.Products.DTOs;
using InventarioBackend.src.Core.Application.Products.Interfaces;
using InventarioBackend.src.Core.Domain.Products.Entities;
using InventarioBackend.src.Core.Domain.Products.Interfaces;
using Mapster;
using System.Security.Claims;

namespace InventarioBackend.src.Core.Application.Products.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CategoryDto>> GetAllAsync() =>
            (await _repository.GetAllAsync()).Adapt<List<CategoryDto>>();

        public async Task<CategoryDto?> GetByIdAsync(Guid id) =>
            (await _repository.GetByIdAsync(id))?.Adapt<CategoryDto>();

        public async Task<Guid> CreateAsync(Category dto)
        {
            var category = dto.Adapt<Category>();
            await _repository.AddAsync(dto); 
            return category.CategoryId; // Assuming Id is set in the AddAsync method
        }

        public async Task<bool> UpdateAsync(Guid id, CategoryUpdateDto dto)
        {
            Category? entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new Exception("Not found");

            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.UpdatedAt = DateTime.UtcNow;
            await _repository.UpdateAsync(entity);

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id){
            await _repository.DeleteAsync(id);
            return true;
        } 
    }

}