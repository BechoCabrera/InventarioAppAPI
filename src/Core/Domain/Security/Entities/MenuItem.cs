using InventarioBackend.src.Core.Domain.Security.Entities;

public class MenuItem
{
    public Guid MenuItemId { get; set; }
    public string Name { get; set; } = null!;
    public string Route { get; set; } = null!;
    public string Icon { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string? BadgeColor { get; set; }
    public string? BadgeValue { get; set; }
    public string? LabelColor { get; set; }
    public string? LabelValue { get; set; }

    public Guid? ParentId { get; set; }
    public MenuItem? Parent { get; set; }
    public ICollection<MenuItem> Children { get; set; } = new List<MenuItem>();

    public ICollection<MenuItemPermission> MenuItemPermissions { get; set; } = new List<MenuItemPermission>();
}

