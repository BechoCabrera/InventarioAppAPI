namespace InventarioBackend.src.Core.Application.Security.DTOs
{
    public class LoginResponse
    {
        public string AccessToken { get; set; } = null!;
        public DateTime Expiration { get; set; }
    }
}
