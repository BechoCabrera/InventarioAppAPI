using InventarioBackend.src.Core.Application.Security.DTOs;
using InventarioBackend.src.Core.Application.Security.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace InventarioBackend.src.Host.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // <-- ESTE ATRIBUTO ES LO QUE FALTABA
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetUser()
        {
            var user = await _userService.GetCurrentUserAsync(User);
            return Ok(user);
        }

        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<UserListDto>>> GetAll(
       CancellationToken cancellationToken)
        {
            var users = await _userService.GetAllAsync(cancellationToken);
            return Ok(users);
        }

        [HttpPost] // POST /api/users
        public async Task<ActionResult<UserListDto>> Create(
        [FromBody] CreateUserRequest request,
        CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _userService.CreateAsync(request, cancellationToken);
            return Ok(created);
        }
    }
}