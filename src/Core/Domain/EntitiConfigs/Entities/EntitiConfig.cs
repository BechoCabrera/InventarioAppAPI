namespace InventarioBackend.src.Core.Domain.EntitiConfigs.Entities
{
    public class EntitiConfig
    {
        public Guid EntitiConfigId { get; set; }
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public string EntitiName { get; set; } = null!;
        public string EntitiNit { get; set; } = null!;
        public string? EntitiAddress { get; set; }
    }

}
