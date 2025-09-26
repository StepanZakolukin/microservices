using Microsoft.AspNetCore.Mvc;
using Saritasa.Tools.Common.Pagination;
using TaskService.Api.Controllers.Task.Request;
using TaskService.Api.Controllers.Task.Response;
using TaskService.BusinessLogic;
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
    
    /// <summary>
    /// Получить список задач (с фильтрацией и пагинацией)
    /// </summary>
    [HttpGet]
    [ProducesResponseType<PagedList<TaskResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTaskListAsync([FromQuery] PageQueryFilter filter, CancellationToken cancellationToken)
    {
        return Ok((await _taskManager.GetTaskListAsync(filter, cancellationToken)).Convert(Convert));
    }
    
    /// <summary>
    /// Получить задачу по ID
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType<TaskResponse>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTaskAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var task = await _taskManager.GetTaskAsync(id, cancellationToken);
        return Ok(Convert(task));
    }
    
    /// <summary>
    /// Создать задачу
    /// </summary>
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
    
    /// <summary>
    /// Обновить задачу
    /// </summary>
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
    
    /// <summary>
    /// Удалить задачу
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteTaskAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await _taskManager.DeleteTaskAsync(id, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Назначить исполнителя задачи
    /// </summary>
    [HttpPut("{id}/assign")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> AssignPerformerAsync(
        [FromRoute] Guid id,
        [FromBody] UserRequest user,
        CancellationToken cancellationToken
    )
    {
        await _taskManager.AssignPerformerAsync(id, user.PerformerId, cancellationToken);
        return NoContent();
    }
    
    private TaskResponse Convert(Domain.Entities.Task task) => new()
    {
        Id = task.Id,
        Title = task.Title,
        Description = task.Description,
        PerformerId = task.PerformerId,
        CreatorId = task.CreatorId,
    };
}