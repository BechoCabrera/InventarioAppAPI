using InventarioBackend.src.Core.Domain.Products;
using InventarioBackend.src.Core.Domain.Security.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventarioBackend.src.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Productos
        public DbSet<Product> Products { get; set; } = null!;

        // Seguridad
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Permission> Permissions => Set<Permission>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relaciones User-Roles (muchos a muchos)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany();

            // Relaciones Role-Permissions (muchos a muchos)
            modelBuilder.Entity<Role>()
                .HasMany(r => r.Permissions)
                .WithMany();

            // Aquí puedes agregar configuraciones adicionales de entidades, índices, etc.
        }
    }
}
