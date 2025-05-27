using InventarioBackend.src.Core.Application.Products.DTOs;
using InventarioBackend.src.Core.Domain.Products;
using Mapster;

namespace InventarioBackend.src.Core.Application._Common.Mappings
{
    public class ProductMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Product, ProductDto>();
            config.NewConfig<ProductCreateDto, Product>();
            config.NewConfig<ProductUpdateDto, Product>()
                  .Ignore(dest => dest.CreatedAt); // No tocar esta propiedad al actualizar
        }
    }
}
