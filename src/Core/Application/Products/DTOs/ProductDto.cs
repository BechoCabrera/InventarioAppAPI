using InventarioBackend.src.Core.Domain.Security.Entities;

namespace InventarioBackend.src.Core.Application.Products.DTOs
{
    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int Stock { get; set; }
        public string? Category { get; set; }
        public Boolean IsActive { get; set; }
        public Guid RegUserId { get; set; }
        public Guid? EntitiId { get; set; }

        public string BarCode { get; set; } = default!;  
        public string? EntitiName { get; set; }
        public string? Username { get; set; }
        public string? CategoryName { get; set; }
    }
}
