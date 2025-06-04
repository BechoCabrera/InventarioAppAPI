namespace InventarioBackend.src.Core.Application.Products.DTOs
{
    public class ProductUpdateDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int Stock { get; set; }
        public Guid? CategoryId { get; set; }
        public bool IsActive { get; set; }
    }
}