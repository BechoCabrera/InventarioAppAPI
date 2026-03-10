namespace InventarioBackend.src.Core.Application.Security.DTOs
{
    public class UserListDto
    {
        public Guid UserId { get; set; }
        public Guid? EntitiId { get; set; }
        public string Username { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Avatar { get; set; }
        public bool IsActive { get; set; }
    }
}
