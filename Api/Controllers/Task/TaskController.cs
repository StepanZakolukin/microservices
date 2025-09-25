using Api.Controllers.Task.Request;
using Api.Controllers.Task.Response;
using BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Saritasa.Tools.Common.Pagination;
using BusinessLogic.Interfaces;

namespace Api.Controllers.Task;

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
    [ProducesResponseType<PagedList<TaskResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTaskListAsync([FromQuery] PageQueryFilter filter, CancellationToken cancellationToken)
    {
        return Ok((await _taskManager.GetTaskListAsync(filter, cancellationToken)).Convert(Convert));
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType<TaskResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTaskAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var task = await _taskManager.GetTaskAsync(id, cancellationToken);
        return Ok(Convert(task));
    }

    private TaskResponse Convert(Domain.Entities.Task task) => new()
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            PerformerId = task.PerformerId,
            CreatorId = task.CreatorId,
        };
    
    [HttpPost]
    [ProducesResponseType<Guid>(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateTaskAsync(
        [FromBody] CreateTaskRequest taskDto,
        CancellationToken cancellationToken
    )
    {
        var id = await _taskManager.CreateTaskAsync(
            taskDto.Title,
            taskDto.Description,
            taskDto.CreatorId,
            cancellationToken);
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