using Microsoft.AspNetCore.Mvc;
using Saritasa.Tools.Common.Pagination;
using TaskService.Api.Controllers.Task.Request;
using TaskService.Api.Controllers.Task.Responce;
using TaskService.BusinessLogic.Interfaces;

namespace TaskService.Api.Controllers.Task;

[ApiController]
[Route("api/tasks")]
public class TaskController : ControllerBase
{
    private readonly ITaskManager _taskManager;
    
    public TaskController(ITaskManager taskManager)
    {
        _taskManager = taskManager;
    }
    
    [HttpGet]
    [ProducesResponseType<PagedListMetadataDto<TaskResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTaskListAsync([FromQuery] TaskRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType<TaskResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTaskAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var task = await _taskManager.GetTaskAsync(id, cancellationToken);
        var result = new TaskResponse
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            PerformerId = task.PerformerId,
        };
        return Ok(result);
    }
    
    [HttpPost]
    [ProducesResponseType<Guid>(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateTaskAsync(
        [FromBody] CreateTaskRequest taskDto,
        CancellationToken cancellationToken
    )
    {
        var id = await _taskManager.CreateTaskAsync(taskDto.Title, taskDto.Description, cancellationToken);
        return Created("api/tasks", id);
    }
    
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateTaskAsync(
        [FromRoute] Guid id,
        [FromBody] TaskRequest taskDto,
        CancellationToken cancellationToken
    )
    {
        await _taskManager.UpdateTaskAsync(id, taskDto.Title, taskDto.Description, cancellationToken);
        return Ok();
    }
    
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteTaskAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await _taskManager.DeleteTaskAsync(id, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// назначить исполнителя
    /// </summary>
    [HttpPut("{id}/assign")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> AssignPerformerAsync(
        [FromRoute] Guid id,
        [FromBody] UserRequest user,
        CancellationToken cancellationToken
    )
    {
        await _taskManager.AssignPerformerAsync(id, user.Id, cancellationToken);
        return NoContent();
    }
}