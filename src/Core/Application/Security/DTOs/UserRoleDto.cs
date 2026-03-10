namespace InventarioBackend.src.Core.Application.Security.DTOs
{
    public class UserRoleDto
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; } = null!;
    }
}
