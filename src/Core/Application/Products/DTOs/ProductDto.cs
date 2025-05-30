using InventarioBackend.src.Core.Domain.Security.Entities;

namespace InventarioBackend.src.Core.Application.Products.DTOs
{
    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? Category { get; set; }
        public Boolean IsActive { get; set; }
        public Guid RegUserId { get; set; }
        public string BarCode { get; set; } = default!;  
        public string? Username { get; set; }
        public string? CategoryName { get; set; }
    }
}
