namespace InventarioBackend.src.Core.Application.Promotions.DTOs
{
    public class CalculatePromotionResponseDto
    {
        public decimal DiscountAmount { get; set; }
        public string PromotionName { get; set; }
        public decimal Percentage { get; set; } // <-- Nuevo campo
        public string PromotionsNames{ get; set; } // <-- Nuevo campo

    }
}
