namespace InventarioBackend.src.Core.Application.Security.Login.DTOs;

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}
