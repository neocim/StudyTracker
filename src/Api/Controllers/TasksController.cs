using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("tasks")]
public class TasksController : ApiController
{
    [HttpGet("new")]
    public IActionResult NewTask()
    {
        return Content("New task");
    }
}
