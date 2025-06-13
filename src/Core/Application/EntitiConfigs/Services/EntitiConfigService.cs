using InventarioBackend.Core.Application.Billing.DTOs;
using InventarioBackend.src.Core.Application.EntitiConfigs.DTOs;
using InventarioBackend.src.Core.Application.EntitiConfigs.Interfaces;
using InventarioBackend.src.Core.Domain.EntitiConfigs.Entities;
using InventarioBackend.src.Core.Domain.EntitiConfigs.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace InventarioBackend.src.Core.Application.EntitiConfigs.Services
{
    public class EntitiConfigService : IEntitiConfigService
    {
        private readonly IEntitiConfigRepository _repository;

        public EntitiConfigService(IEntitiConfigRepository repository)
        {
            _repository = repository;
        }

        public async Task<EntitiConfigDto?> GetByCodeAsync(string code)
        {
            var entity = await _repository.GetByCodeAsync(code);
            return entity?.Adapt<EntitiConfigDto>();
        }
        public async Task<List<EntitiConfigDto>> GetAllAsync()
        {
            var entity = await _repository.GetAllAsync();
            return entity.Adapt<List<EntitiConfigDto>>();
        } 
        public async Task<EntitiConfigDto> GetByIdEntitiAsync(Guid id)
        {
            var entity = await _repository.GetByIdEntitiAsync(id);
            return entity.Adapt<EntitiConfigDto>();
        }
        public async Task<EntitiConfigDto> CreateAsync(EntitiConfigCreateDto dto)
        {
            var entity = dto.Adapt<EntitiConfig>();
            entity.EntitiConfigId = Guid.NewGuid();
            var saved = await _repository.AddAsync(entity);
            return saved.Adapt<EntitiConfigDto>();
        }

        public async Task<EntitiConfigDto> UpdateAsync(EntitiConfigUpdateDto dto)
        {
            var entity = dto.Adapt<EntitiConfig>();
            entity.EntitiConfigId = dto.Id;
            var updated = await _repository.UpdateAsync(entity);
            return updated.Adapt<EntitiConfigDto>();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null)
            {
                return await _repository.DeleteAsync(entity);
            }
            else
            {
                return false;
            }
        }
    }

}
