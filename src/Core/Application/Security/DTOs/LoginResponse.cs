namespace InventarioBackend.src.Core.Application.Security.DTOs
{
    public class LoginResponse
    {
        public string access_token { get; set; } = null!;
        public DateTime Expiration { get; set; }

        public int expires_in { get; set; }
    }
}
