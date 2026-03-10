namespace InventarioBackend.src.Core.Application.Security.DTOs
{
    public class RoleDto
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public string? Description { get; set; }

        // Opcional: para devolver qué permisos tiene el rol
        public IList<Guid>? PermissionIds { get; set; }
    }
}
