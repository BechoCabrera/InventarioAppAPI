using InventarioBackend.src.Core.Domain.Billing.Entities;
using InventarioBackend.src.Core.Domain.EntitiConfigs.Entities;
using InventarioBackend.src.Core.Domain.Security.Entities;

namespace InventarioBackend.src.Core.Application.Billing.DTOs
{
    public class InvoiceCancellationDto
    {
        public Guid InvoiceCancellationId { get; set; }  // ID único de la anulación
        public Guid InvoiceId { get; set; }
        public string Reason { get; set; }
        public DateTime? CancellationDate { get; set; }
        public Guid? CancelledByUserId { get; set; }
        public Guid EntitiConfigId { get; set; }
        //public Invoice? Invoice { get; set; }
        //public User? User { get; set; }
        public string? InvoiceNumber { get; set; }
        public string? CancelledByUser { get; set; }
        //public EntitiConfig? EntitiConfigs { get; set; }

    }
}