using InventarioBackend.src.Core.Application.Security.DTOs;
using InventarioBackend.src.Core.Application.Security.Interfaces;
using InventarioBackend.src.Core.Domain.Security.Entities;
using InventarioBackend.src.Core.Domain.Security.Interfaces;
using InventarioBackend.src.Infrastructure.Data.Repositories.Security;

namespace InventarioBackend.src.Core.Application.Security.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRoleService(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        public async Task<IEnumerable<UserRoleDto>> GetByUserAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var list = await _userRoleRepository.GetByUserIdAsync(userId, cancellationToken);

            return list.Select(ur => new UserRoleDto
            {
                UserId = ur.UserId,
                RoleId = ur.RoleId,
                RoleName = ur.Role.RoleName
            });
        }

        public async Task UpdateUserRolesAsync(UpdateUserRolesRequest request, CancellationToken cancellationToken = default)
        {
            var current = await _userRoleRepository.GetByUserIdAsync(request.UserId, cancellationToken);

            await _userRoleRepository.RemoveRangeAsync(current, cancellationToken);

            var toAdd = request.RoleIds
                .Distinct()
                .Select(roleId => new UserRole
                {
                    UserId = request.UserId,
                    RoleId = roleId
                })
                .ToList();

            await _userRoleRepository.AddRangeAsync(toAdd, cancellationToken);
            await _userRoleRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
