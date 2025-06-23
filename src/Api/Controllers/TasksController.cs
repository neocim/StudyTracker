using Api.Dto.Requests.Task;
using Api.Dto.Responses.Task;
using Application.Cqrs.Commands.Task;
using Application.Cqrs.Queries.Task;
using Application.Dto.Task;
using Microsoft.AspNetCore.Mvc;
using Entity = Domain.Entities;
using MediatR;

namespace Api.Controllers;

[Route("task")]
public class TasksController(IMediator mediator) : ApiController
{
    [HttpPost]
    public async Task<ActionResult<TaskCreatedResult>> NewTask(NewTaskRequest request)
    {
        var task = new Entity.Task(request!.OwnerId, request.FromDate, request.ToDate,
            request.Description, request.Name);

        var command = new AddNewTaskCommand(request.OwnerId, task);
        var result = await mediator.Send(command);

        return result.Match(_ => Ok(TaskCreatedResult.FromTask(task)), Error);
    }

    [HttpGet("{taskId:guid}")]
    public async Task<ActionResult<TaskResult>> GetTask(GetTaskRequest request)
    {
        var query = new GetTaskByIdQuery(request!.TaskId);
        var result = await mediator.Send(query);

        return result.Match(task => Ok(task), Error);
    }
}