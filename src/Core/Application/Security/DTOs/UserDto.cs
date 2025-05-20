namespace InventarioBackend.src.Core.Application.Security.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public IList<string> Roles { get; set; } = new List<string>();
    }
}
