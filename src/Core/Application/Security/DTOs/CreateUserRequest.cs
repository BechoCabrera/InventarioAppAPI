namespace InventarioBackend.src.Core.Application.Security.DTOs
{
    public class CreateUserRequest
    {
        public Guid? EntitiId { get; set; }
        public string Username { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string? Avatar { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
