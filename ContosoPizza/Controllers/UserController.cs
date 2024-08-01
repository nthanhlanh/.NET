using ContosoPizza.Helprs;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ApiExplorerSettings(GroupName = "v1")]
    public class UserController : ControllerBase
    {
        private readonly IApplicationUserService _userService;

        public UserController(IApplicationUserService userService)
        {
            _userService = userService;
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult> GetUsers([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;
            
            var (users, totalCount) = await _userService.GetUsersAsync(page, pageSize);

            var response = new
            {
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                Users = users
            };

            return Ok(response);
        }

        [HttpGet("details")]
        public async Task<IActionResult> GetUserDetails()
        {
            var token = HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");

            if (_userService.IsTokenExpired(token))
            {
                return Unauthorized(new { message = "Token has expired." });
            }

            var user = await _userService.GetUserFromTokenAsync(token);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
