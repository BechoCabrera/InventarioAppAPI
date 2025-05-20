using InventarioBackend.src.Core.Domain.Security.Entities;

namespace InventarioBackend.src.Core.Domain.Security.Interfaces
{
    public interface ITokenService
    {
        TokenResult GenerateToken(User user);
    }

    public class TokenResult
    {
        public string TokenString { get; set; } = null!;
        public DateTime Expiration { get; set; }
    }
}
