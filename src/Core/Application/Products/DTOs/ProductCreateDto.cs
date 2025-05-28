using InventarioBackend.src.Core.Domain.Security.Entities;

namespace InventarioBackend.src.Core.Application.Products.DTOs
{
    public class ProductCreateDto
    {
        public string? Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? BarCode { get; set; } = default!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public bool IsActive { get; set; }
        public Guid? CategoryId { get; set; }
        public User? User { get; set; }
    }
}