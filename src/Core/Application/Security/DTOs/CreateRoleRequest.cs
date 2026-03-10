namespace InventarioBackend.src.Core.Application.Security.DTOs
{
    public class CreateRoleRequest
    {
        public string RoleName { get; set; } = null!;
        public string? Description { get; set; }

        // Opcional: permisos iniciales para el rol
        public IList<Guid>? PermissionIds { get; set; }
    }
}
