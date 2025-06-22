using Api.Dto.Requests.Task;
using Application.Cqrs.Commands.Task;
using Application.Cqrs.Queries.Task;
using Application.Dto.Task;
using Microsoft.AspNetCore.Mvc;
using Entity = Domain.Entities;
using MediatR;

namespace Api.Controllers;

[Route("task")]
public class TasksController(Mediator mediator) : ApiController
{
    [HttpPost]
    public async Task<ActionResult> NewTask(HttpContext context)
    {
        var request = await context.Request.ReadFromJsonAsync<NewTaskRequest>();

        var task = new Entity.Task(request!.OwnerId, request.FromDate, request.ToDate,
            request.Description, request.Name, request.Success);

        var command = new AddNewTaskCommand(request.OwnerId, task);
        var result = await mediator.Send(command);

        return result.Match(created => Ok(), Error);
    }

    [HttpGet("{taskId:guid}")]
    public async Task<ActionResult<TaskResult>> GetTask(HttpContext context)
    {
        var request = await context.Request.ReadFromJsonAsync<GetTaskRequest>();

        var query = new GetTaskByIdQuery(Guid.Parse(request!.TaskId));
        var result = await mediator.Send(query);

        return result.Match(task => Ok(task), Error);
    }
}