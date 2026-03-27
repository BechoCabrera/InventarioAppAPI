using System;

namespace InventarioBackend.src.Core.Domain.Billing.Entities
{
    public class InvoicePayment
    {
        public Guid InvoicePaymentId { get; set; } = Guid.NewGuid();
        public Guid InvoiceId { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public decimal Amount { get; set; }

        public Invoice? Invoice { get; set; }
    }
}
