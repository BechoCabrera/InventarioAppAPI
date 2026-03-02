namespace InventarioBackend.src.Core.Application.Promotions.DTOs
{
    public class PromotionCreateDto
    {
        public string Name { get; set; }
        public int Type { get; set; }
        public decimal Percentage { get; set; }
        public int? MinQuantity { get; set; }
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<Guid> ProductIds { get; set; }
    }
}
