using InventarioBackend.Core.Domain.Billing;
using InventarioBackend.src.Core.Application.EntitiConfigs.Services;
using InventarioBackend.src.Core.Domain.EntitiConfigs.Entities;
using InventarioBackend.src.Core.Domain.Products.Entities;
using InventarioBackend.src.Core.Domain.Security.Entities;

namespace InventarioBackend.src.Core.Domain.Products
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public Guid? EntitiId { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal UnitPrice { get; set; }
        public int Stock { get; set; }
        public int StockSold { get; set; }
        public Guid? CategoryId { get; set; }       
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public string? BarCode { get; set; }
        public Guid RegUserId { get; set; }
        public User User { get; set; } = default!;
        public Category? Category { get; set; }
        public EntitiConfig? EntitiConfigs { get; set; }
        public ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
        //public EntitiConfig? EntitiConfigs { get; set; }


    }
}
