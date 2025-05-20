namespace InventarioBackend.src.Core.Application.Security.DTOs
{
    public class LoginRequest
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
