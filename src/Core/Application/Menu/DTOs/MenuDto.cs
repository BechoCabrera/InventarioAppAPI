namespace InventarioBackend.src.Core.Application.Menu.DTOs
{
    public class MenuDto
    {
        public string Route { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Icon { get; set; } = null!;
        public MenuTagDto? Label { get; set; }
        public MenuTagDto? Badge { get; set; }
        public List<MenuChildrenItemDto>? Children { get; set; }
        public MenuPermissionsDto? Permissions { get; set; }
    }
}
