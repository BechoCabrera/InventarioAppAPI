using InventarioBackend.src.Core.Domain.EntitiConfigs.Entities;
using InventarioBackend.src.Core.Domain.Security.Entities;

namespace InventarioBackend.src.Core.Domain.Billing.Entities
{
    public class InvoicesCancelled
    {
        public Guid InvoiceCancelledId { get; set; }  // ID único de la anulación
        public Guid InvoiceId { get; set; }  // Relación con la factura original
        public string Reason { get; set; }   // Motivo de la anulación
        public DateTime CancellationDate { get; set; }  // Fecha en la que se hizo la anulación
        public Guid CancelledByUserId { get; set; }  // Usuario que realizó la anulación
        public Guid EntitiConfigId { get; set; }  // Usuario que realizó la anulación
        public EntitiConfig EntitiConfigs { get; set; }  // Usuario que realizó la anulación
        // Propiedades de navegación (si es necesario)
        public Invoice Invoice { get; set; }  // Relación con la factura original
        public User CancelledByUser { get; set; }  // Usuario que hizo la anulación
    }
}
