using InventarioBackend.src.Core.Application.EntitiConfigs.DTOs;
using InventarioBackend.src.Core.Domain.EntitiConfigs.Entities;
using Mapster;

namespace InventarioBackend.src.Core.Application._Common.Mappings
{
    public class EntitiConfigMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<EntitiConfig, EntitiConfigDto>();
            config.NewConfig<EntitiConfigCreateDto, EntitiConfig>()
                  .Ignore(dest => dest.EntitiConfigId);
            config.NewConfig<EntitiConfigUpdateDto, EntitiConfig>();
        }
    }
}
