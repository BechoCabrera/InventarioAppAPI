using InventarioBackend.src.Core.Application.Products.DTOs;
using InventarioBackend.src.Core.Domain.Products;
using Mapster;

namespace InventarioBackend.src.Core.Application._Common.Mappings
{
    public class ProductMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // Mapeo de entidad a DTO incluyendo el nombre del usuario
            config.NewConfig<Product, ProductDto>()
              .Map(dest => dest.Username, src => src.User.Name);

            config.NewConfig<ProductCreateDto, Product>();
            config.NewConfig<ProductUpdateDto, Product>()
                  .Ignore(dest => dest.CreatedAt); // No tocar esta propiedad al actualizar
        }
    }
}
