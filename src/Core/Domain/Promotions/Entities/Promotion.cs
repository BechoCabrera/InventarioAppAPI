using InventarioBackend.src.Core.Domain.Promotions.Enums;

namespace InventarioBackend.src.Core.Domain.Promotions.Entities
{
    public class Promotion
    {
        public Guid PromotionId { get; set; }   // 👈 AGREGA ESTO

        public string Name { get; set; }

        public int Type { get; set; }

        public decimal Percentage { get; set; }

        public int? MinQuantity { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }

        public Guid EntitiId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public ICollection<PromotionProduct> PromotionProducts { get; set; }
    }
}
