using Api.Dto.Requests.Task;
using Api.Dto.Responses.Task;
using Application.Cqrs.Commands.Task;
using Application.Cqrs.Queries.Task;
using Application.Dto.Task;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Entity = Domain.Entities;

namespace Api.Controllers;

[Authorize]
[Route("users/{userId:guid}")]
public class TasksController(IMediator mediator) : ApiController
{
    [HttpPost("tasks")]
    public async Task<ActionResult<TaskResponse>> NewTask(Guid userId,
        NewTaskRequest request)
    {
        var task = new Entity.Task(Guid.NewGuid(), userId, request.BeginDate,
            request.EndDate, request.Name,
            request.Description, request.Success);

        var command = new AddNewTaskCommand(task);
        var result = await mediator.Send(command);

        var response = TaskResponse.FromTaskEntity(task);
        var routeValues = new { taskId = task.Id, request = new GetTaskRequest() };

        return result.Match(_ => CreatedAtAction(nameof(GetTask), routeValues, response),
            Error);
    }

    [HttpPost("tasks/{parentTaskId:guid}/subtask")]
    public async Task<ActionResult<TaskResponse>> AddSubTask(Guid userId,
        Guid parentTaskId,
        AddSubTaskRequest request)
    {
        var subTask = new SubTask(Guid.NewGuid(), parentTaskId, userId, request.BeginDate,
            request.EndDate, request.Name,
            request.Description, request.Success);

        var command = new AddSubTaskCommand(subTask);
        var result = await mediator.Send(command);

        var response = TaskResponse.FromTaskEntity(subTask.ToTaskEntity());
        var routeValues = new { taskId = subTask.Id, request = new GetTaskRequest() };

        return result.Match(_ => CreatedAtAction(nameof(GetTask), routeValues, response),
            Error);
    }

    [HttpGet("tasks/{taskId:guid}")]
    public async Task<ActionResult<Entity.Task>> GetTask(Guid taskId,
        GetTaskRequest request)
    {
        var query = new GetTaskByIdQuery(taskId);
        var result = await mediator.Send(query);

        return result.Match(task => Ok(task), Error);
    }

    [HttpPatch("tasks/{taskId:guid}")]
    public async Task<ActionResult> UpdateTask(Guid taskId, UpdateTaskRequest request)
    {
        var command = new UpdateTaskCommand(taskId, request.Success, request.Name,
            request.Description);
        var result = await mediator.Send(command);

        return result.Match(_ => Ok(), Error);
    }

    [HttpDelete("tasks/{taskId:guid}")]
    public async Task<ActionResult> DeleteTask(Guid taskId)
    {
        var command = new RemoveTaskCommand(taskId);
        var result = await mediator.Send(command);

        return result.Match(_ => Ok(), Error);
    }
}