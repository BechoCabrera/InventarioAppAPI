namespace InventarioBackend.src.Core.Domain.Security.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!; // Guarda el hash de la contraseña
        public List<Role> Roles { get; set; } = new();

        // Método para validar password (compara hash)
        public bool ValidatePassword(string password)
        {
            // Aquí debes implementar la validación de hash real, por ejemplo con BCrypt
            // Ejemplo ficticio (reemplaza con tu lógica real):
            return PasswordHash == HashPassword(password);
        }

        private string HashPassword(string password)
        {
            // Implementa hash real, aquí solo ejemplo
            return password; // << REEMPLAZAR
        }
    }
}
