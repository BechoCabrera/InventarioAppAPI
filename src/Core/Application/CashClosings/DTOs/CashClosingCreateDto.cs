namespace InventarioBackend.src.Core.Application.CashClosings.DTOs
{
    public class CashClosingCreateDto
    {
        public DateTime Date { get; set; }
        public decimal TotalCash { get; set; }
        public decimal TotalCredit { get; set; }
        public decimal TotalCard { get; set; }
        public decimal TotalTransfer { get; set; }
        public Guid? EntitiId { get; set; }
    }
}
