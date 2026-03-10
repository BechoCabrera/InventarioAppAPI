namespace InventarioBackend.src.Core.Application.Security.DTOs
{
    public class CreatePermissionRequest
    {
        public string PermissionName { get; set; } = null!;
        public string? Description { get; set; }
    }
}
