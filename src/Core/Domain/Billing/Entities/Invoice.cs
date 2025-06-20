using InventarioBackend.Core.Domain.Billing;
using InventarioBackend.src.Core.Domain.Clients.Entities;
using InventarioBackend.src.Core.Domain.EntitiConfigs.Entities;

namespace InventarioBackend.src.Core.Domain.Billing.Entities
{
    public class Invoice
    {
        public Guid InvoiceId { get; set; } = Guid.NewGuid();
        public string InvoiceNumber { get; set; } = string.Empty;
        public Guid? ClientId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal SubtotalAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? PaymentMethod { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? EntitiId { get; set; }
        public string? NameClientDraft { get; set; }
        public string? NitClientDraft { get; set; }
        // Navegación
        public List<InvoiceDetail> Details { get; set; } = new();
        public Client Client { get; set; } = default!;
        public EntitiConfig? EntitiConfigs { get; set; } = default!;
    }
}
