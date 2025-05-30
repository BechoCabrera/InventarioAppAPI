namespace InventarioBackend.src.Core.Application.Products.DTOs
{
    public class CategoryDto
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
    }
}
