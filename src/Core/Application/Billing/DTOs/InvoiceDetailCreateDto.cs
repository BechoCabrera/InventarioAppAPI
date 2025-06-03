using System;

namespace InventarioBackend.Core.Application.Billing.DTOs
{
    public class InvoiceDetailCreateDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
