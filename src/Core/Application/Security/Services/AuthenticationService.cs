using System.Threading.Tasks;
using InventarioBackend.src.Core.Application.Security.DTOs;
using InventarioBackend.src.Core.Domain.Security.Entities;
using InventarioBackend.src.Core.Domain.Security.Interfaces;
using InventarioBackend.src.Core.Application.Security.Interfaces;

namespace InventarioBackend.src.Core.Application.Security.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthenticationService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetByUsernameAsync(request.Username);
            if (user == null || !user.ValidatePassword(request.Password))
                throw new UnauthorizedAccessException("Credenciales inválidas");

            var token = _tokenService.GenerateToken(user);

            return new LoginResponse
            {
                AccessToken = token.TokenString,
                Expiration = token.Expiration
            };
        }

        public Task LogoutAsync(string userId)
        {
            // Opcional: implementar lógica para invalidar tokens, auditar logout, etc.
            return Task.CompletedTask;
        }
    }
}
