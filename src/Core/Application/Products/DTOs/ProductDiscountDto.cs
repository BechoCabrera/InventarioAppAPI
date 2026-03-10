namespace InventarioBackend.src.Core.Application.Products.DTOs
{
    public class ProductDiscountDto
    {
        public Guid? ProductId { get; set; }
        public Guid PromotionId { get; set; }
        public string PromotionName { get; set; }
        public decimal Percentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public decimal Discount { get; set; }
        public int? MinRequired { get; set; } // <-- Nuevo campo


    }
}
