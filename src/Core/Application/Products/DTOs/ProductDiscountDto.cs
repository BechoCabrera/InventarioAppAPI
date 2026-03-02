namespace InventarioBackend.src.Core.Application.Products.DTOs
{
    public class ProductDiscountDto
    {
        public Guid PromotionId { get; set; }
        public string PromotionName { get; set; }
        public decimal Percentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
