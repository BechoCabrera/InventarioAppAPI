namespace InventarioBackend.src.Core.Application.Products.DTOs
{
    public class CategoryUpdateDto
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
    }
}
