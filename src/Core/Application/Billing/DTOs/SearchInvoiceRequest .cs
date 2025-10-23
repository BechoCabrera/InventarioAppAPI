using InventarioBackend.src.Core.Domain.Billing.Entities;

using InventarioBackend.Core.Application.Billing.DTOs;

namespace InventarioBackend.src.Core.Application.Billing.DTOs
{
    public class SearchInvoiceRequest
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? PaymentMethod { get; set; }
        public bool? IsCancelled { get; set; }
        public string? InvoiceNumber { get; set; }
    }
}
