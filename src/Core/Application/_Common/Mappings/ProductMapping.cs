using InventarioBackend.src.Core.Application.Clients.DTOs;
using InventarioBackend.src.Core.Application.Products.DTOs;
using InventarioBackend.src.Core.Domain.Clients.Entities;
using InventarioBackend.src.Core.Domain.Products;
using InventarioBackend.src.Core.Domain.Products.Entities;
using Mapster;

namespace InventarioBackend.src.Core.Application._Common.Mappings
{
    public class ProductMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // Mapeo de entidad a DTO incluyendo el nombre del usuario
            config.NewConfig<Product, ProductDto>()
              .Map(dest => dest.Username, src => src.User.Name)
              .Map(dest => dest.CategoryName, src => src.Category!.Name)
              .Map(dest => dest.EntitiName, src => src.EntitiConfigs.EntitiName);
                ;

            config.NewConfig<Category, CategoryDto>()
            .Map(dest => dest.EntitiName, src => src.EntitiConfigs.EntitiName); 
            ;
            config.NewConfig<ProductCreateDto, Product>();
            config.NewConfig<ProductUpdateDto, Product>()
                  .Ignore(dest => dest.CreatedAt); // No tocar esta propiedad al actualizar

            config.NewConfig<Client, ClientDto>();
            config.NewConfig<ClientCreateDto, Client>();
            config.NewConfig<ClientUpdateDto, Client>();
        }
    }
}
