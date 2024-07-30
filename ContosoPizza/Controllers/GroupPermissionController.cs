using Microsoft.AspNetCore.Mvc;
using ContosoPizza.Services;
namespace ContosoPizza.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupPermissionController : ControllerBase
    {
        private readonly IGroupPermissionService _groupPermissionService;

        public GroupPermissionController(IGroupPermissionService groupPermissionService)
        {
            _groupPermissionService = groupPermissionService;
        }

        [HttpGet("{groupId}/permissions")]
        public async Task<IActionResult> GetPermissionsByGroupId(int groupId)
        {
            var permissions = await _groupPermissionService.GetPermissionsByGroupIdAsync(groupId);
            return Ok(permissions);
        }

        [HttpPost("{groupId}/permissions/{permissionId}")]
        public async Task<IActionResult> AddPermissionToGroup(int groupId, int permissionId)
        {
            await _groupPermissionService.AddPermissionToGroupAsync(groupId, permissionId);
            return Ok(new { message = "Permission added successfully." });
        }

        [HttpDelete("{groupId}/permissions/{permissionId}")]
        public async Task<IActionResult> RemovePermissionFromGroup(int groupId, int permissionId)
        {
            await _groupPermissionService.RemovePermissionFromGroupAsync(groupId, permissionId);
            return Ok(new { message = "Permission removed successfully." });
        }
    }
}