using Api.Dto.Requests.Task;
using Api.Dto.Responses.Task;
using Application.Cqrs.Commands.Task;
using Application.Cqrs.Queries.Task;
using Application.Dto.Task;
using Entity = Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Api.Controllers;

[Route("task")]
public class TaskController(IMediator mediator) : ApiController
{
    [HttpPost]
    public async Task<ActionResult<TaskCreatedResponse>> NewTask(NewTaskRequest request)
    {
        var task = new Entity.Task(Guid.NewGuid(), request.BeginDate,
            request.EndDate,
            request.Description, request.Name, request.Success);

        var command = new AddNewTaskCommand(request.OwnerId, task);
        var result = await mediator.Send(command);

        return result.Match(_ => Ok(TaskCreatedResponse.FromTask(task)), Error);
    }

    [HttpPost("subtask")]
    public async Task<ActionResult<TaskCreatedResponse>> AddSubTask(
        AddSubTaskRequest request)
    {
        var task = new Entity.Task(Guid.NewGuid(), request.BeginDate, request.EndDate,
            request.Description, request.Name, request.Success);

        var command = new AddSubTaskCommand(request.ParentTaskId, task);
        var result = await mediator.Send(command);

        return result.Match(_ => Ok(TaskCreatedResponse.FromTask(task)), Error);
    }

    [HttpPatch]
    public async Task<ActionResult> UpdateTask(UpdateTaskRequest request)
    {
        var command = new UpdateTaskCommand(request.TaskId, request.Success, request.Name,
            request.Description);
        var result = await mediator.Send(command);

        return result.Match(_ => Ok(), Error);
    }

    [HttpDelete("{taskId:guid}")]
    public async Task<ActionResult> DeleteTask(Guid taskId)
    {
        var command = new RemoveTaskCommand(taskId);
        var result = await mediator.Send(command);

        return result.Match(_ => Ok(), Error);
    }

    [HttpGet("{taskId:guid}")]
    public async Task<ActionResult<TaskResult>> GetTask(GetTaskRequest request)
    {
        var query = new GetTaskByIdQuery(request.TaskId);
        var result = await mediator.Send(query);

        return result.Match(task => Ok(task), Error);
    }
}