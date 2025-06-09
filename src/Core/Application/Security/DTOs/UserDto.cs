namespace InventarioBackend.src.Core.Application.Security.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public Guid? EntitiId { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Avatar { get; set; } = null!;
        public IList<string> Roles { get; set; } = new List<string>();
        public List<string> Permissions { get; set; } = new List<string>();

    }
}
