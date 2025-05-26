
using InventarioBackend.src.Core.Application.Menu.DTOs;
using InventarioBackend.src.Core.Application.Menu.Interfaces;
using InventarioBackend.src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioBackend.Core.Application.Menu.Services
{
    public class MenuService : IMenuService
    {
        private readonly AppDbContext _context;

        //public MenuService()
        //{
        //    _menu = new List<MenuDto>
        //        {
        //            new MenuDto
        //            {
        //                Route = "dashboard",
        //                Name = "dashboard",
        //                Type = "link",
        //                Icon = "dashboard",
        //                Badge = new MenuTagDto { Color = "red-50", Value = "5" }
        //            },
        //            new MenuDto
        //            {
        //                Route = "design",
        //                Name = "design",
        //                Type = "sub",
        //                Icon = "color_lens",
        //                Label = new MenuTagDto { Color = "azure-50", Value = "New" },
        //                Children = new List<MenuChildrenItemDto>
        //                {
        //                    new MenuChildrenItemDto { Route = "colors", Name = "colors", Type = "link" },
        //                    new MenuChildrenItemDto { Route = "icons", Name = "icons", Type = "link" }
        //                }
        //            },
        //        new MenuDto
        //        {
        //            Route = "material",
        //            Name = "material",
        //            Type = "sub",
        //            Icon = "favorite",
        //            Children = new List<MenuChildrenItemDto>
        //            {
        //                new MenuChildrenItemDto
        //                {
        //                    Route = "",
        //                    Name = "form-controls",
        //                    Type = "sub",
        //                    Children = new List<MenuChildrenItemDto>
        //                    {
        //                        new MenuChildrenItemDto { Route = "autocomplete", Name = "autocomplete", Type = "link" },
        //                        new MenuChildrenItemDto { Route = "checkbox", Name = "checkbox", Type = "link" },
        //                        new MenuChildrenItemDto { Route = "datepicker", Name = "datepicker", Type = "link" },
        //                        new MenuChildrenItemDto { Route = "form-field", Name = "form-field", Type = "link" },
        //                        new MenuChildrenItemDto { Route = "input", Name = "input", Type = "link" },
        //                        new MenuChildrenItemDto { Route = "radio", Name = "radio", Type = "link" },
        //                        new MenuChildrenItemDto { Route = "select", Name = "select", Type = "link" },
        //                        new MenuChildrenItemDto { Route = "slider", Name = "slider", Type = "link" },
        //                        new MenuChildrenItemDto { Route = "slide-toggle", Name = "slide-toggle", Type = "link" }
        //                    }
        //                },
        //                new MenuChildrenItemDto
        //                {
        //                    Route = "",
        //                    Name = "navigation",
        //                    Type = "sub",
        //                    Children = new List<MenuChildrenItemDto>
        //                    {
        //                        new MenuChildrenItemDto { Route = "menu", Name = "menu", Type = "link" },
        //                        new MenuChildrenItemDto { Route = "sidenav", Name = "sidenav", Type = "link" },
        //                        new MenuChildrenItemDto { Route = "toolbar", Name = "toolbar", Type = "link" }
        //                    }
        //                },
        //                new MenuChildrenItemDto
        //                {
        //                    Route = "",
        //                    Name = "layout",
        //                    Type = "sub",
        //                    Children = new List<MenuChildrenItemDto>
        //                    {
        //                        new MenuChildrenItemDto { Route = "card", Name = "card", Type = "link" },
        //                        new MenuChildrenItemDto { Route = "divider", Name = "divider", Type = "link" },
        //                        new MenuChildrenItemDto { Route = "expansion", Name = "expansion", Type = "link" },
        //                        new MenuChildrenItemDto { Route = "grid-list", Name = "grid-list", Type = "link" },
        //                        new MenuChildrenItemDto { Route = "list", Name = "list", Type = "link" },
        //                        new MenuChildrenItemDto { Route = "stepper", Name = "stepper", Type = "link" },
        //                        new MenuChildrenItemDto { Route = "tab", Name = "tab", Type = "link" },
        //                        new MenuChildrenItemDto { Route = "tree", Name = "tree", Type = "link" }
        //                    }
        //                },
        //                new MenuChildrenItemDto
        //                {
        //                    Route = "",
        //                    Name = "buttons-indicators",
        //                    Type = "sub",
        //                    Children = new List<MenuChildrenItemDto>
        //                    {
        //                        new MenuChildrenItemDto { Route = "button", Name = "button", Type = "link" },
        //                        new MenuChildrenItemDto { Route = "button-toggle", Name = "button-toggle", Type = "link" },
        //                        new MenuChildrenItemDto { Route = "badge", Name = "badge", Type = "link" },
        //                        new MenuChildrenItemDto { Route = "chips", Name = "chips", Type = "link" },
        //                        new MenuChildrenItemDto { Route = "icon", Name = "icon", Type = "link" },
        //                        new MenuChildrenItemDto { Route = "progress-spinner", Name = "progress-spinner", Type = "link" },
        //                        new MenuChildrenItemDto { Route = "progress-bar", Name = "progress-bar", Type = "link" },
        //                        new MenuChildrenItemDto { Route = "ripple", Name = "ripple", Type = "link" }
        //                    }
        //                },
        //                new MenuChildrenItemDto
        //                {
        //                    Route = "",
        //                    Name = "popups-modals",
        //                    Type = "sub",
        //                    Children = new List<MenuChildrenItemDto>
        //                    {
        //                        new MenuChildrenItemDto { Route = "bottom-sheet", Name = "bottom-sheet", Type = "link" },
        //                        new MenuChildrenItemDto { Route = "dialog", Name = "dialog", Type = "link" },
        //                        new MenuChildrenItemDto { Route = "snack-bar", Name = "snackbar", Type = "link" },
        //                        new MenuChildrenItemDto { Route = "tooltip", Name = "tooltip", Type = "link" }
        //                    }
        //                },
        //                new MenuChildrenItemDto
        //                {
        //                    Route = "data-table",
        //                    Name = "data-table",
        //                    Type = "sub",
        //                    Children = new List<MenuChildrenItemDto>
        //                    {
        //                        new MenuChildrenItemDto { Route = "paginator", Name = "paginator", Type = "link" },
        //                        new MenuChildrenItemDto { Route = "sort", Name = "sort", Type = "link" },
        //                        new MenuChildrenItemDto { Route = "table", Name = "table", Type = "link" }
        //                    }
        //                }
        //            }
        //        },
        //        new MenuDto
        //        {
        //            Route = "permissions",
        //            Name = "permissions",
        //            Type = "sub",
        //            Icon = "lock",
        //            Children = new List<MenuChildrenItemDto>
        //            {
        //                new MenuChildrenItemDto { Route = "role-switching", Name = "role-switching", Type = "link" },
        //                new MenuChildrenItemDto
        //                {
        //                    Route = "route-guard",
        //                    Name = "route-guard",
        //                    Type = "link"
        //                },
        //                new MenuChildrenItemDto
        //                {
        //                    Route = "test",
        //                    Name = "test",
        //                    Type = "link"
        //                }
        //            }
        //        },
        //        new MenuDto
        //        {
        //            Route = "media",
        //            Name = "media",
        //            Type = "sub",
        //            Icon = "image",
        //            Children = new List<MenuChildrenItemDto>
        //            {
        //                new MenuChildrenItemDto { Route = "gallery", Name = "gallery", Type = "link" }
        //            }
        //        },
        //        new MenuDto
        //        {
        //            Route = "forms",
        //            Name = "forms",
        //            Type = "sub",
        //            Icon = "description",
        //            Children = new List<MenuChildrenItemDto>
        //            {
        //                new MenuChildrenItemDto { Route = "elements", Name = "form-elements", Type = "link" },
        //                new MenuChildrenItemDto { Route = "dynamic", Name = "dynamic-form", Type = "link" },
        //                new MenuChildrenItemDto { Route = "select", Name = "select", Type = "link" },
        //                new MenuChildrenItemDto { Route = "datetime", Name = "datetime", Type = "link" }
        //            }
        //        },
        //        new MenuDto
        //        {
        //            Route = "tables",
        //            Name = "tables",
        //            Type = "sub",
        //            Icon = "format_line_spacing",
        //            Children = new List<MenuChildrenItemDto>
        //            {
        //                new MenuChildrenItemDto { Route = "kitchen-sink", Name = "kitchen-sink", Type = "link" },
        //                new MenuChildrenItemDto { Route = "remote-data", Name = "remote-data", Type = "link" }
        //            }
        //        },
        //        new MenuDto
        //        {
        //            Route = "profile",
        //            Name = "profile",
        //            Type = "sub",
        //            Icon = "person",
        //            Permissions = new MenuPermissionsDto { Only = new[] { "BUSINESS_OWNER" } },
        //            Children = new List<MenuChildrenItemDto>
        //            {
        //                new MenuChildrenItemDto { Route = "overview", Name = "overview", Type = "link" },
        //                new MenuChildrenItemDto { Route = "settings", Name = "settings", Type = "link" }
        //            }
        //        },
        //        new MenuDto
        //        {
        //            Route = "https://ng-matero.github.io/extensions/",
        //            Name = "extensions",
        //            Type = "extTabLink",
        //            Icon = "extension"
        //        },
        //        new MenuDto
        //        {
        //            Route = "/",
        //            Name = "sessions",
        //            Type = "sub",
        //            Icon = "question_answer",
        //            Children = new List<MenuChildrenItemDto>
        //            {
        //                new MenuChildrenItemDto { Route = "403", Name = "403", Type = "link" },
        //                new MenuChildrenItemDto { Route = "404", Name = "404", Type = "link" },
        //                new MenuChildrenItemDto { Route = "500", Name = "500", Type = "link" }
        //            }
        //        },
        //        new MenuDto
        //        {
        //            Route = "utilities",
        //            Name = "utilities",
        //            Type = "sub",
        //            Icon = "all_inbox",
        //            Children = new List<MenuChildrenItemDto>
        //            {
        //                new MenuChildrenItemDto { Route = "css-grid", Name = "css-grid", Type = "link" },
        //                new MenuChildrenItemDto { Route = "css-helpers", Name = "css-helpers", Type = "link" }
        //            }
        //        },
        //        new MenuDto
        //        {
        //            Route = "menu-level",
        //            Name = "menu-level",
        //            Type = "sub",
        //            Icon = "subject",
        //            Children = new List<MenuChildrenItemDto>
        //            {
        //                new MenuChildrenItemDto
        //                {
        //                    Route = "level-1-1",
        //                    Name = "level-1-1",
        //                    Type = "sub",
        //                    Children = new List<MenuChildrenItemDto>
        //                    {
        //                        new MenuChildrenItemDto
        //                        {
        //                            Route = "level-2-1",
        //                            Name = "level-2-1",
        //                            Type = "sub",
        //                            Children = new List<MenuChildrenItemDto>
        //                            {
        //                                new MenuChildrenItemDto
        //                                {
        //                                    Route = "level-3-1",
        //                                    Name = "level-3-1",
        //                                    Type = "sub",
        //                                    Children = new List<MenuChildrenItemDto>
        //                                    {
        //                                        new MenuChildrenItemDto { Route = "level-4-1", Name = "level-4-1", Type = "link" }
        //                                    }
        //                                }
        //                            }
        //                        },
        //                        new MenuChildrenItemDto { Route = "level-2-2", Name = "level-2-2", Type = "link" }
        //                    }
        //                },
        //                new MenuChildrenItemDto { Route = "level-1-2", Name = "level-1-2", Type = "link" }
        //            }
        //        },
        //         new MenuDto
        //{
        //    Route = "clientes",
        //    Name = "clientes",
        //    Type = "link",
        //    Icon = "group",
        //    Permissions = new MenuPermissionsDto { Only = new[] { "ADMIN", "BUSINESS_OWNER" } }
        //},
        //new MenuDto
        //{
        //    Route = "inventarios",
        //    Name = "inventarios",
        //    Type = "link",
        //    Icon = "inventory",
        //    Permissions = new MenuPermissionsDto { Only = new[] { "ADMIN", "BUSINESS_OWNER" } }
        //},
        //new MenuDto
        //{
        //    Route = "productos",
        //    Name = "productos",
        //    Type = "link",
        //    Icon = "shopping_cart",
        //    Permissions = new MenuPermissionsDto { Only = new[] { "ADMIN", "BUSINESS_OWNER" } }
        //}

        //    };

        //    // Aplica permiso solo ADMIN a todos los items del menú y sus hijos
        //    ApplyAdminPermission(_menu);
        //}

        public MenuService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<MenuDto>> GetMenuForUserAsync(Guid userId)
        {
            try
            {
                var user = await _context.Users
               .Include(u => u.UserPermissions).ThenInclude(up => up.Permission)
               .Include(u => u.UserRoles).ThenInclude(ur => ur.Role).ThenInclude(r => r.RolePermissions).ThenInclude(rp => rp.Permission)
               .FirstOrDefaultAsync(u => u.UserId == userId);

                if (user == null)
                    return new List<MenuDto>();

                var isAdmin = user.UserRoles.Any(r => r.Role.RoleName == "ADMIN");

                var permissions = user.UserPermissions
                    .Select(up => up.Permission.PermissionName)
                    .Union(user.UserRoles.SelectMany(ur => ur.Role.RolePermissions.Select(rp => rp.Permission.PermissionName)))
                    .Distinct()
                    .ToList();

                IQueryable<MenuItem> query = _context.MenuItems
          .Include(m => m.Children)
          .Include(m => m.MenuItemPermissions)
              .ThenInclude(mp => mp.Permission)
          .Where(m => m.IsActive);

                var corruptos = query.SelectMany(m => m.MenuItemPermissions)
    .Where(mp => mp.Permission == null)
    .ToList();

                Console.WriteLine($"PERMISOS NULOS: {corruptos.Count}");

                if (!isAdmin)
                {
                    query = query.Where(m =>
                        m.MenuItemPermissions.Any(mp => mp.Permission != null && permissions.Contains(mp.Permission.PermissionName)));
                }
                Console.WriteLine("Permisos: " + string.Join(", ", permissions));
                List<MenuItem> menuItems = await query.ToListAsync();

                var menuTree = menuItems
                    .Where(m => m.ParentId == null)
                    .Select(m => MapToDtoRecursive(m, menuItems))
                    .ToList();

                return menuTree;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private MenuDto MapToDtoRecursive(MenuItem item, List<MenuItem> allItems)
        {
            return new MenuDto
            {
                Name = item.Name,
                Route = item.Route,
                Icon = item.Icon,
                Type = item.Type,
                Badge = string.IsNullOrEmpty(item.BadgeValue) ? null : new MenuTagDto
                {
                    Color = item.BadgeColor ?? string.Empty,
                    Value = item.BadgeValue
                },
                Label = string.IsNullOrEmpty(item.LabelValue) ? null : new MenuTagDto
                {
                    Color = item.LabelColor ?? string.Empty,
                    Value = item.LabelValue
                },
                Permissions = new MenuPermissionsDto
                {
                    Only = item.MenuItemPermissions
                        .Where(mp => mp.Permission != null && !string.IsNullOrEmpty(mp.Permission.PermissionName))
                        .Select(mp => mp.Permission.PermissionName)
                        .ToArray()
                },
                Children = allItems
                    .Where(child => child.ParentId == item.MenuItemId)
                    .Select(child => MapToChildDtoRecursive(child, allItems))
                    .ToList()
            };
        }


        private MenuChildrenItemDto MapToChildDtoRecursive(MenuItem item, List<MenuItem> allItems)
        {
            return new MenuChildrenItemDto
            {
                Name = item.Name ?? string.Empty,
                Route = item.Route ?? string.Empty,
                Type = item.Type ?? string.Empty,
                Children = allItems
                    .Where(child => child.ParentId != null && child.ParentId == item.MenuItemId)
                    .Select(child => MapToChildDtoRecursive(child, allItems))
                    .ToList()
            };
        }

    }
}
