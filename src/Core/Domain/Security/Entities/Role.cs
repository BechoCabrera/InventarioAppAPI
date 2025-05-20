namespace InventarioBackend.src.Core.Domain.Security.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<Permission> Permissions { get; set; } = new();
    }
}
