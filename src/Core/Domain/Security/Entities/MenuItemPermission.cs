﻿namespace InventarioBackend.src.Core.Domain.Security.Entities
{
    public class MenuItemPermission
    {
        public Guid MenuItemId { get; set; }
        public Guid PermissionId { get; set; }

        public MenuItem MenuItem { get; set; } = default!;
        public Permission Permission { get; set; } = default!;

    }
}
