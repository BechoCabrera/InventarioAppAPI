namespace InventarioBackend.src.Core.Application.Security.DTOs
{
    public class UpdateUserRolesRequest
    {
        public Guid UserId { get; set; }
        public IList<Guid> RoleIds { get; set; } = new List<Guid>();
    }
}
