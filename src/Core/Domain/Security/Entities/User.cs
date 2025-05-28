using InventarioBackend.src.Core.Domain.Products;

namespace InventarioBackend.src.Core.Domain.Security.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Avatar { get; set; }
        public string PasswordHash { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
        // Relación muchos a muchos con Roles
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        // Relación muchos a muchos con Permisos
        public ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
        public bool ValidatePassword(string password)
        {
            // Aquí debes implementar la validación de hash real, por ejemplo con BCrypt
            // Ejemplo ficticio (reemplaza con tu lógica real):
            return PasswordHash == password;
        }

        private string HashPassword(string password)
        {
            // Implementa hash real, aquí solo ejemplo
            return password; // << REEMPLAZAR
        }
    }
}
