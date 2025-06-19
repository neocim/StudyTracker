using Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Task = Domain.Entities.Task;
using Domain.Entities;
using MediatR;

namespace Api.Controllers;

[Route("task")]
public class TasksController(Mediator mediator) : ApiController
{
    [HttpGet("new")]
    public async Task<IActionResult> NewTask(ApplicationDbContext context)
    {
        var taskId = Guid.NewGuid();
    }

    [HttpGet("{taskId:guid}")]
    public async Task<IActionResult> GetTask(Guid taskId, ApplicationDbContext context)
    {
        var task = await context.Tasks.FindAsync(taskId);
        return new JsonResult(task);
    }
}