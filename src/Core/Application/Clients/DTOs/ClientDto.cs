namespace InventarioBackend.src.Core.Application.Clients.DTOs
{
    public class ClientDto
    {
        public Guid ClientId { get; set; }
        public string Name { get; set; } = default!;
        public string Nit { get; set; } = default!;
        public string? Email { get; set; } = default!;
        public string? Phone { get; set; } = default!;
        public bool IsActive { get; set; }
        public Guid? EntitiId { get; set; }
        public string EntitiName { get; set; } = default!;
    }
}
