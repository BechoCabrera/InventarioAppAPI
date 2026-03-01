using InventarioBackend.src.Core.Domain.Promotions.Enums;

namespace InventarioBackend.src.Core.Application.Promotions.DTOs
{
    public class CreatePromotionDto
    {
        public string Name { get; set; }
        public PromotionType Type { get; set; }
        public decimal Percentage { get; set; }
        public int? MinQuantity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Guid> ProductIds { get; set; }
    }
}
