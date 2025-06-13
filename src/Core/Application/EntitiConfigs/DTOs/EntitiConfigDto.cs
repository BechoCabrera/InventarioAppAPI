namespace InventarioBackend.src.Core.Application.EntitiConfigs.DTOs
{
    public class EntitiConfigDto 
    {
        public Guid EntitiConfigId { get; set; }
        public string? Code { get; set; } = default!;
        public string? EntitiName { get; set; } = null!;
        public string EntitiNit { get; set; } = null!;
        public string? EntitiAddress { get; set; }
        public string? Description { get; set; }
        public string? EntitiPhone { get; set; }
    }
}
