using Api.Dto.Requests;
using Application.Cqrs.Commands.Task;
using Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Entity = Domain.Entities;
using Domain.Entities;
using ErrorOr;
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

        return result.Match(success => Ok(), Error);
    }

    [HttpGet("{taskId:guid}")]
    public async Task<ActionResult> GetTask(Guid taskId)
    {
    }
}