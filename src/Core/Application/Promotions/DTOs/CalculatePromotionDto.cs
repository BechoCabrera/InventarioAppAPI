namespace InventarioBackend.src.Core.Application.Promotions.DTOs
{
    public class CalculatePromotionDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
