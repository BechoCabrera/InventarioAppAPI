using System;
using InventarioBackend.src.Core.Domain.Billing.Entities;
using InventarioBackend.src.Core.Domain.Products;

namespace InventarioBackend.Core.Domain.Billing
{
    public class InvoiceDetail
    {
        public Guid InvoiceDetailId { get; set; } = Guid.NewGuid();
        public Guid InvoiceId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // TotalPrice calculado en la base, pero puedes calcularlo aquí también
        public decimal TotalPrice => Quantity * UnitPrice;

        // Navegación
        public Invoice? Invoice { get; set; }
        public Product Product { get; set; } = null!;
    }
}
