namespace InventarioBackend.src.Core.Application.Promotions.DTOs
{
    public class PromotionDto
    {
        public Guid PromotionId { get; set; }
        public string Name { get; set; }
        public int Type { get; set; } // O PromotionType si quieres el enum
        public decimal Percentage { get; set; }
        public int? MinQuantity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public List<Guid> ProductIds { get; set; } // Opcional, si quieres mostrar los productos asociados
        public List<PromotionProductDto?> Products { get; set; } = new List<PromotionProductDto?>();
    }
}
