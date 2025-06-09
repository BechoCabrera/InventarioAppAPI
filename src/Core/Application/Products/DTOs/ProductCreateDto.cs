using InventarioBackend.src.Core.Domain.EntitiConfigs.Entities;
using InventarioBackend.src.Core.Domain.Security.Entities;

namespace InventarioBackend.src.Core.Application.Products.DTOs
{
    public class ProductCreateDto
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string BarCode { get; set; } = default!;
        public decimal UnitPrice { get; set; }
        public int Stock { get; set; }
        public bool IsActive { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? EntitiId { get; set; }
        public User? User { get; set; } = default!;
        //public EntitiConfig? EntitiConfigs { get; set; } = default!;

    }
}