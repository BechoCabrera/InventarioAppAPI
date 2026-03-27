using System;

namespace InventarioBackend.Core.Application.Billing.DTOs
{
    public class InvoicePaymentDto
    {
        public Guid? InvoicePaymentId { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
