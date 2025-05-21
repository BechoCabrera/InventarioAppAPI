namespace InventarioBackend.src.Core.Application.Security.DTOs
{
    public class TokenResult
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
