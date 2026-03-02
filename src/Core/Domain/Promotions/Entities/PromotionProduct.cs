using InventarioBackend.src.Core.Domain.Products;

namespace InventarioBackend.src.Core.Domain.Promotions.Entities
{
    public class PromotionProduct
    {
        public Guid PromotionId { get; set; }
        public Promotion Promotion { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; } // <-- Asegúrate de tener esto

    }
}
