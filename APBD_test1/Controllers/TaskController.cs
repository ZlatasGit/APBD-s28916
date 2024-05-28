using Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Controllers;
[Route("api/")]
[ApiController]
public class TaskController : ControllerBase
{
    
    private ITaskRepository _taskRepository;

    public TaskController(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }


    [HttpGet("tasks/")]
    public async Task<IActionResult> GetTasksToPerform(int? idProject = null)
    {
        var tasks = await _taskRepository.GetTasks(idProject);
        if (tasks == null)
        {
            return NotFound("No tasks found.");
        }
        return Ok(tasks);
    }

    [HttpPost("tasks/")]
    public async Task<IActionResult> AddTeamMember([FromBody] TeamMember teamMember)
    {
        int newTeamMemberId = await _taskRepository.AddTeamMember(teamMember);
        if (newTeamMemberId == 0)
        {
            return BadRequest("Failed to add team member.");
        }
        return Ok(newTeamMemberId);
    }
    [HttpPut("tasks/{id}")]
    public async Task<IActionResult> EditTask(int id, [FromBody] TaskModel task)
    {
        // Update the task in the database
        await _taskRepository.UpdateTask(id, task);
            return Ok($"Task with id {id} has been updated.");
    }
}