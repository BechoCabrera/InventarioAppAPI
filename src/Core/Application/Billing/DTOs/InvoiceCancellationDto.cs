namespace InventarioBackend.src.Core.Application.Billing.DTOs
{
    public class InvoiceCancellationDto
    {
        public Guid InvoiceId { get; set; }
        public string Reason { get; set; }
        public DateTime CancellationDate { get; set; }
        public Guid CancellationByUserId { get; set; }
    }
}
