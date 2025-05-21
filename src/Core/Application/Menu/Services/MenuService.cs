using InventarioBackend.src.Core.Application.Menu.DTOs;
using InventarioBackend.src.Core.Application.Menu.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace InventarioBackend.Core.Application.Menu.Services;
public class MenuService : IMenuService
{
    private readonly List<MenuDto> _menu;

    public MenuService()
    {
        _menu = new List<MenuDto>
        {
            new MenuDto
            {
                Route = "dashboard",
                Name = "dashboard",
                Type = "link",
                Icon = "dashboard",
                Badge = new MenuTagDto { Color = "red-50", Value = "5" }
            },
            new MenuDto
            {
                Route = "design",
                Name = "design",
                Type = "sub",
                Icon = "color_lens",
                Label = new MenuTagDto { Color = "azure-50", Value = "New" },
                Children = new List<MenuChildrenItemDto>
                {
                    new MenuChildrenItemDto { Route = "colors", Name = "colors", Type = "link" },
                    new MenuChildrenItemDto { Route = "icons", Name = "icons", Type = "link" }
                }
            },
            new MenuDto
            {
                Route = "material",
                Name = "material",
                Type = "sub",
                Icon = "favorite",
                Children = new List<MenuChildrenItemDto>
                {
                    new MenuChildrenItemDto
                    {
                        Route = "",
                        Name = "form-controls",
                        Type = "sub",
                        Children = new List<MenuChildrenItemDto>
                        {
                            new MenuChildrenItemDto { Route = "autocomplete", Name = "autocomplete", Type = "link" },
                            new MenuChildrenItemDto { Route = "checkbox", Name = "checkbox", Type = "link" },
                            new MenuChildrenItemDto { Route = "datepicker", Name = "datepicker", Type = "link" },
                            new MenuChildrenItemDto { Route = "form-field", Name = "form-field", Type = "link" },
                            new MenuChildrenItemDto { Route = "input", Name = "input", Type = "link" },
                            new MenuChildrenItemDto { Route = "radio", Name = "radio", Type = "link" },
                            new MenuChildrenItemDto { Route = "select", Name = "select", Type = "link" },
                            new MenuChildrenItemDto { Route = "slider", Name = "slider", Type = "link" },
                            new MenuChildrenItemDto { Route = "slide-toggle", Name = "slide-toggle", Type = "link" }
                        }
                    },
                    new MenuChildrenItemDto
                    {
                        Route = "",
                        Name = "navigation",
                        Type = "sub",
                        Children = new List<MenuChildrenItemDto>
                        {
                            new MenuChildrenItemDto { Route = "menu", Name = "menu", Type = "link" },
                            new MenuChildrenItemDto { Route = "sidenav", Name = "sidenav", Type = "link" },
                            new MenuChildrenItemDto { Route = "toolbar", Name = "toolbar", Type = "link" }
                        }
                    },
                    new MenuChildrenItemDto
                    {
                        Route = "",
                        Name = "layout",
                        Type = "sub",
                        Children = new List<MenuChildrenItemDto>
                        {
                            new MenuChildrenItemDto { Route = "card", Name = "card", Type = "link" },
                            new MenuChildrenItemDto { Route = "divider", Name = "divider", Type = "link" },
                            new MenuChildrenItemDto { Route = "expansion", Name = "expansion", Type = "link" },
                            new MenuChildrenItemDto { Route = "grid-list", Name = "grid-list", Type = "link" },
                            new MenuChildrenItemDto { Route = "list", Name = "list", Type = "link" },
                            new MenuChildrenItemDto { Route = "stepper", Name = "stepper", Type = "link" },
                            new MenuChildrenItemDto { Route = "tab", Name = "tab", Type = "link" },
                            new MenuChildrenItemDto { Route = "tree", Name = "tree", Type = "link" }
                        }
                    },
                    new MenuChildrenItemDto
                    {
                        Route = "",
                        Name = "buttons-indicators",
                        Type = "sub",
                        Children = new List<MenuChildrenItemDto>
                        {
                            new MenuChildrenItemDto { Route = "button", Name = "button", Type = "link" },
                            new MenuChildrenItemDto { Route = "button-toggle", Name = "button-toggle", Type = "link" },
                            new MenuChildrenItemDto { Route = "badge", Name = "badge", Type = "link" },
                            new MenuChildrenItemDto { Route = "chips", Name = "chips", Type = "link" },
                            new MenuChildrenItemDto { Route = "icon", Name = "icon", Type = "link" },
                            new MenuChildrenItemDto { Route = "progress-spinner", Name = "progress-spinner", Type = "link" },
                            new MenuChildrenItemDto { Route = "progress-bar", Name = "progress-bar", Type = "link" },
                            new MenuChildrenItemDto { Route = "ripple", Name = "ripple", Type = "link" }
                        }
                    },
                    new MenuChildrenItemDto
                    {
                        Route = "",
                        Name = "popups-modals",
                        Type = "sub",
                        Children = new List<MenuChildrenItemDto>
                        {
                            new MenuChildrenItemDto { Route = "bottom-sheet", Name = "bottom-sheet", Type = "link" },
                            new MenuChildrenItemDto { Route = "dialog", Name = "dialog", Type = "link" },
                            new MenuChildrenItemDto { Route = "snack-bar", Name = "snackbar", Type = "link" },
                            new MenuChildrenItemDto { Route = "tooltip", Name = "tooltip", Type = "link" }
                        }
                    },
                    new MenuChildrenItemDto
                    {
                        Route = "data-table",
                        Name = "data-table",
                        Type = "sub",
                        Children = new List<MenuChildrenItemDto>
                        {
                            new MenuChildrenItemDto { Route = "paginator", Name = "paginator", Type = "link" },
                            new MenuChildrenItemDto { Route = "sort", Name = "sort", Type = "link" },
                            new MenuChildrenItemDto { Route = "table", Name = "table", Type = "link" }
                        }
                    }
                }
            },
            new MenuDto
            {
                Route = "permissions",
                Name = "permissions",
                Type = "sub",
                Icon = "lock",
                Children = new List<MenuChildrenItemDto>
                {
                    new MenuChildrenItemDto { Route = "role-switching", Name = "role-switching", Type = "link" },
                    new MenuChildrenItemDto
                    {
                        Route = "route-guard",
                        Name = "route-guard",
                        Type = "link"
                    },
                    new MenuChildrenItemDto
                    {
                        Route = "test",
                        Name = "test",
                        Type = "link"
                    }
                }
            },
            new MenuDto
            {
                Route = "media",
                Name = "media",
                Type = "sub",
                Icon = "image",
                Children = new List<MenuChildrenItemDto>
                {
                    new MenuChildrenItemDto { Route = "gallery", Name = "gallery", Type = "link" }
                }
            },
            new MenuDto
            {
                Route = "forms",
                Name = "forms",
                Type = "sub",
                Icon = "description",
                Children = new List<MenuChildrenItemDto>
                {
                    new MenuChildrenItemDto { Route = "elements", Name = "form-elements", Type = "link" },
                    new MenuChildrenItemDto { Route = "dynamic", Name = "dynamic-form", Type = "link" },
                    new MenuChildrenItemDto { Route = "select", Name = "select", Type = "link" },
                    new MenuChildrenItemDto { Route = "datetime", Name = "datetime", Type = "link" }
                }
            },
            new MenuDto
            {
                Route = "tables",
                Name = "tables",
                Type = "sub",
                Icon = "format_line_spacing",
                Children = new List<MenuChildrenItemDto>
                {
                    new MenuChildrenItemDto { Route = "kitchen-sink", Name = "kitchen-sink", Type = "link" },
                    new MenuChildrenItemDto { Route = "remote-data", Name = "remote-data", Type = "link" }
                }
            },
            new MenuDto
            {
                Route = "profile",
                Name = "profile",
                Type = "sub",
                Icon = "person",
                Permissions = new MenuPermissionsDto { Only = new[] { "BUSINESS_OWNER" } },
                Children = new List<MenuChildrenItemDto>
                {
                    new MenuChildrenItemDto { Route = "overview", Name = "overview", Type = "link" },
                    new MenuChildrenItemDto { Route = "settings", Name = "settings", Type = "link" }
                }
            },
            new MenuDto
            {
                Route = "https://ng-matero.github.io/extensions/",
                Name = "extensions",
                Type = "extTabLink",
                Icon = "extension"
            },
            new MenuDto
            {
                Route = "/",
                Name = "sessions",
                Type = "sub",
                Icon = "question_answer",
                Children = new List<MenuChildrenItemDto>
                {
                    new MenuChildrenItemDto { Route = "403", Name = "403", Type = "link" },
                    new MenuChildrenItemDto { Route = "404", Name = "404", Type = "link" },
                    new MenuChildrenItemDto { Route = "500", Name = "500", Type = "link" }
                }
            },
            new MenuDto
            {
                Route = "utilities",
                Name = "utilities",
                Type = "sub",
                Icon = "all_inbox",
                Children = new List<MenuChildrenItemDto>
                {
                    new MenuChildrenItemDto { Route = "css-grid", Name = "css-grid", Type = "link" },
                    new MenuChildrenItemDto { Route = "css-helpers", Name = "css-helpers", Type = "link" }
                }
            },
            new MenuDto
            {
                Route = "menu-level",
                Name = "menu-level",
                Type = "sub",
                Icon = "subject",
                Children = new List<MenuChildrenItemDto>
                {
                    new MenuChildrenItemDto
                    {
                        Route = "level-1-1",
                        Name = "level-1-1",
                        Type = "sub",
                        Children = new List<MenuChildrenItemDto>
                        {
                            new MenuChildrenItemDto
                            {
                                Route = "level-2-1",
                                Name = "level-2-1",
                                Type = "sub",
                                Children = new List<MenuChildrenItemDto>
                                {
                                    new MenuChildrenItemDto
                                    {
                                        Route = "level-3-1",
                                        Name = "level-3-1",
                                        Type = "sub",
                                        Children = new List<MenuChildrenItemDto>
                                        {
                                            new MenuChildrenItemDto { Route = "level-4-1", Name = "level-4-1", Type = "link" }
                                        }
                                    }
                                }
                            },
                            new MenuChildrenItemDto { Route = "level-2-2", Name = "level-2-2", Type = "link" }
                        }
                    },
                    new MenuChildrenItemDto { Route = "level-1-2", Name = "level-1-2", Type = "link" }
                }
            },
             new MenuDto
    {
        Route = "clientes",
        Name = "clientes",
        Type = "link",
        Icon = "group",
        Permissions = new MenuPermissionsDto { Only = new[] { "ADMIN", "BUSINESS_OWNER" } }
    },
    new MenuDto
    {
        Route = "inventarios",
        Name = "inventarios",
        Type = "link",
        Icon = "inventory",
        Permissions = new MenuPermissionsDto { Only = new[] { "ADMIN", "BUSINESS_OWNER" } }
    },
    new MenuDto
    {
        Route = "productos",
        Name = "productos",
        Type = "link",
        Icon = "shopping_cart",
        Permissions = new MenuPermissionsDto { Only = new[] { "ADMIN", "BUSINESS_OWNER" } }
    }

        };

        // Aplica permiso solo ADMIN a todos los items del menú y sus hijos
        ApplyAdminPermission(_menu);
    }

    public List<MenuDto> GetMenuForUser(string[] userRoles)
    {
        var filteredMenu = new List<MenuDto>();

        if (userRoles == null) return filteredMenu;

        if (userRoles.Contains("ADMIN"))
        {
            // ADMIN ve todo el menú
            filteredMenu = _menu;
        }

        else if (userRoles.Contains("OWNER"))
        {
            // Filtrar menú para OWNER
            filteredMenu = _menu
                .Where(item => item.Permissions != null
                               && item.Permissions.Only != null
                               && item.Permissions.Only.Contains("OWNER"))
                .ToList();
        }
        return new List<MenuDto>(); // Vacío si no es ADMIN
    }

    private void ApplyAdminPermission(List<MenuDto> menuItems)
    {
        foreach (var item in menuItems)
        {
            item.Permissions = new MenuPermissionsDto { Only = new[] { "ADMIN" } };

            if (item.Children != null)
                ApplyAdminPermissionToChildren(item.Children);
        }
    }

    private void ApplyAdminPermissionToChildren(List<MenuChildrenItemDto> children)
    {
        foreach (var child in children)
        {
            child.Permissions = new MenuPermissionsDto { Only = new[] { "ADMIN" } };

            if (child.Children != null)
                ApplyAdminPermissionToChildren(child.Children);
        }
    }
}
