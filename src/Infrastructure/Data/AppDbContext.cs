﻿using InventarioBackend.src.Core.Domain.Products;
using InventarioBackend.src.Core.Domain.Security.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventarioBackend.src.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Productos
        public DbSet<Product> Products { get; set; } = null!;

        // Seguridad
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<UserRole> UserRole => Set<UserRole>();
        public DbSet<UserPermission> UserPermissions => Set<UserPermission>();
        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<MenuItem> MenuItems => Set<MenuItem>();
        public DbSet<MenuItemPermission> MenuItemPermissions => Set<MenuItemPermission>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // UserPermission
            modelBuilder.Entity<UserPermission>()
                .HasKey(up => new { up.UserId, up.PermissionId });

            modelBuilder.Entity<UserPermission>()
                .HasOne(up => up.User)
                .WithMany(u => u.UserPermissions)
                .HasForeignKey(up => up.UserId);

            modelBuilder.Entity<UserPermission>()
                .HasOne(up => up.Permission)
                .WithMany(p => p.UserPermissions)
                .HasForeignKey(up => up.PermissionId);

            // RolePermission
            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId);

            modelBuilder.Entity<UserRole>().ToTable("UserRoles");

            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);
            // Productos
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");
            // MenuItem relación jerárquica
            modelBuilder.Entity<MenuItem>()
                 .HasMany(m => m.Children)
                 .WithOne(m => m.Parent)
                 .HasForeignKey(m => m.ParentId)
                 .OnDelete(DeleteBehavior.Restrict);

            // Relación muchos a muchos MenuItem <-> Permission
            modelBuilder.Entity<MenuItemPermission>()
                .HasKey(mp => new { mp.MenuItemId, mp.PermissionId });

            modelBuilder.Entity<MenuItemPermission>()
                .HasOne(mp => mp.MenuItem)
                .WithMany(m => m.MenuItemPermissions)
                .HasForeignKey(mp => mp.MenuItemId);

            modelBuilder.Entity<MenuItemPermission>()
                .HasOne(mp => mp.Permission)
                .WithMany()
                .HasForeignKey(mp => mp.PermissionId);

            // Aquí puedes agregar configuraciones adicionales de entidades, índices, etc.
        }
    }
}
