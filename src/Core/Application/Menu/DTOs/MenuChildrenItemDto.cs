namespace InventarioBackend.src.Core.Application.Menu.DTOs
{
    public class MenuChildrenItemDto
    {
        public string Route { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!; // link, sub, extLink, extTabLink
        public List<MenuChildrenItemDto>? Children { get; set; }
        public MenuPermissionsDto? Permissions { get; set; }
    }
}
