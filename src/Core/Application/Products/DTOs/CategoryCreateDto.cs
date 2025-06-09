namespace InventarioBackend.src.Core.Application.Products.DTOs
{
    public class CategoryCreateDto
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public Guid? EntitiId { get; set; }
    }
}
