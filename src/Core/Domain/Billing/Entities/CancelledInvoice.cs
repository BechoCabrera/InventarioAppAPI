namespace InventarioBackend.src.Core.Domain.Billing.Entities
{
    public class CancelledInvoice
    {
        public Guid InvoiceCancellationId { get; set; } // ID único para la anulación
        public Guid InvoiceId { get; set; } // ID de la factura anulada
        public string Reason { get; set; } // Motivo de la anulación
        public DateTime CancellationDate { get; set; } // Fecha de la anulación
        public Guid CancelledByUserId { get; set; } // Usuario que realizó la anulación
    }
}
