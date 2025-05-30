namespace InventarioBackend.src.Core.Domain.Clients.Entities
{
    public class Client
    {
        public Guid ClientId { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Nit { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
