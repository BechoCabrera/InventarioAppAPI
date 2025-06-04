using InventarioBackend.src.Core.Domain.Products.Entities;
using InventarioBackend.src.Core.Domain.Security.Entities;

namespace InventarioBackend.src.Core.Domain.Products
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal UnitPrice { get; set; }
        public int Stock { get; set; }
        public Guid? CategoryId { get; set; }       
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public string? BarCode { get; set; }
        public Guid RegUserId { get; set; }
        public User User { get; set; } = default!;
        public Category? Category { get; set; }
    }
}
