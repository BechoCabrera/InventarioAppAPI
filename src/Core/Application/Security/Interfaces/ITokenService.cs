using InventarioBackend.src.Core.Application.Security.DTOs;
using InventarioBackend.src.Core.Domain.Security.Entities;

namespace InventarioBackend.src.Core.Application.Security.Interfaces
{
    public interface ITokenService
    {
        TokenResult GenerateToken(User user);
    }
}
