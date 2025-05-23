namespace InventarioBackend.src.Core.Domain.Security.Entities
{
    public class MenuItemPermission
    {
        public Guid MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; } = null!;

        public Guid PermissionId { get; set; }
        public Permission Permission { get; set; } = null!;
    }
}
