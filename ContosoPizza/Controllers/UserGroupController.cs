using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class UserGroupController : ControllerBase
    {
        private readonly IUserGroupService _userGroupService;

        public UserGroupController(IUserGroupService userGroupService)
        {
            _userGroupService = userGroupService;
        }

        [HttpGet("{userId}/groups")]
        public async Task<IActionResult> GetUserGroups(string userId)
        {
            var groups = await _userGroupService.GetUserGroupsAsync(userId);
            return Ok(groups);
        }

        [HttpPost("{userId}/groups/{groupId}")]
        public async Task<IActionResult> AddUserToGroup(string userId, int groupId)
        {
            await _userGroupService.AddUserToGroupAsync(userId, groupId);
            return Ok();
        }

        [HttpDelete("{userId}/groups/{groupId}")]
        public async Task<IActionResult> RemoveUserFromGroup(string userId, int groupId)
        {
            await _userGroupService.RemoveUserFromGroupAsync(userId, groupId);
            return Ok();
        }
    }
}
