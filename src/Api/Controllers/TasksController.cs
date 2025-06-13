using Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Task = Domain.Entities.Task;
using Domain.Entities;

namespace Api.Controllers;

[Route("task")]
public class TasksController : ApiController
{
    [HttpGet("new")]
    public async Task<IActionResult> NewTask(ApplicationDbContext context)
    {
        var guid = Guid.NewGuid();
        var uguid = Guid.NewGuid();
        var user = new User(uguid);
        var task = new Task(guid,
            DateOnly.MaxValue,
            DateOnly.MinValue) { Owner = user };
        await context.AddAsync(task);
        await context.SaveChangesAsync();
        return Content($"New task with guid \n{guid}\nand user guid\n{uguid}");
    }

    [HttpGet("{taskId:guid}")]
    public async Task<IActionResult> GetTask(Guid taskId, ApplicationDbContext context)
    {
        var task = await context.Tasks.FindAsync(taskId);
        return new JsonResult(task);
    }
}