using System;
using System.Collections.Generic;

namespace InventarioBackend.src.Core.Domain.Security.Entities
{
    public class Permission
    {
        public Guid PermissionId { get; set; }  // Identificador único del permiso
        public string PermissionName { get; set; }  // Nombre del permiso
        public string Description { get; set; }  // Descripción del permiso

        // Relación muchos a muchos con Role
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

        // Relación muchos a muchos con User
        public ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();

        // Relación con MenuItemPermission
        public ICollection<MenuItemPermission> MenuItemPermissions { get; set; } = new List<MenuItemPermission>();
    }
}
