namespace InventarioBackend.src.Core.Application.Clients.DTOs
{
    public class ClientCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Nit { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Phone { get; set; } = string.Empty;
        public Guid? EntitiId { get; set; }

    }
}
