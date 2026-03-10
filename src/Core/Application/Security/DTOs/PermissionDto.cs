namespace InventarioBackend.src.Core.Application.Security.DTOs
{
    public class PermissionDto
    {
        public Guid PermissionId { get; set; }
        public string PermissionName { get; set; } = null!;
        public string? Description { get; set; }
    }
}
