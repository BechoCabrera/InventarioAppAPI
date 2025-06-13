namespace InventarioBackend.src.Core.Application.EntitiConfigs.DTOs
{
    public class EntitiConfigCreateDto
    {
        public string Code { get; set; } = null!;
        public string EntitiName { get; set; } = null!;
        public string EntitiNit { get; set; } = null!;
        public string? EntitiAddress { get; set; }
        public string? Description { get; set; }
        public string? EntitiPhone { get; set; }
    }
}
