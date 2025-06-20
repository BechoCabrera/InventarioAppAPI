namespace InventarioBackend.src.Core.Application.CashClosings.DTOs
{
    public class CashClosingDto
    {
        public Guid CashClosingId { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalCash { get; set; }
        public decimal TotalCredit { get; set; }
        public decimal TotalCard { get; set; }
        public decimal TotalTransfer { get; set; }
        public decimal TotalAmount { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? EntitiId { get; set; }
    }
}
