using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGroups()
        {
            var groups = await _groupService.GetAllGroupsAsync();
            return Ok(groups);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroupById(int id)
        {
            var group = await _groupService.GetGroupByIdAsync(id);
            if (group == null)
            {
                return NotFound();
            }
            return Ok(group);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromBody] Group group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdGroup = await _groupService.CreateGroupAsync(group);
            return CreatedAtAction(nameof(GetGroupById), new { id = createdGroup.Id }, createdGroup);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroup(int id, [FromBody] Group group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingGroup = await _groupService.GetGroupByIdAsync(id);
            if (existingGroup == null)
            {
                return NotFound();
            }

            group.Id = id;
            var updatedGroup = await _groupService.UpdateGroupAsync(group);
            return Ok(updatedGroup);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var existingGroup = await _groupService.GetGroupByIdAsync(id);
            if (existingGroup == null)
            {
                return NotFound();
            }

            await _groupService.DeleteGroupAsync(id);
            return NoContent();
        }
    }
}
