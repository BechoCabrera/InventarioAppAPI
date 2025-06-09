using InventarioBackend.src.Core.Domain.EntitiConfigs.Entities;

namespace InventarioBackend.src.Core.Domain.Products.Entities
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public Guid? EntitiId { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public EntitiConfig? EntitiConfigs { get; set; }

    }
}
