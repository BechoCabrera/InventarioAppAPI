
using InventarioBackend.src.Core.Application.CashClosings.DTOs;
using InventarioBackend.src.Core.Application.Clients.DTOs;
using InventarioBackend.src.Core.Domain.CashClosings.Entities;
using InventarioBackend.src.Core.Domain.Clients.Entities;
using Mapster;

namespace InventarioBackend.src.Core.Application._Common.Mappings
{
    public class CashClosingMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // Mapear entidad Client a ClientDto
            config.NewConfig<CashClosing, CashClosingDto>()
                .Map(dest => dest.EntitiName, src => src.EntitiConfigs.EntitiName)
                .Map(dest => dest.UserName, src => src.User.Name)
                ;

            
        }
    }
}
