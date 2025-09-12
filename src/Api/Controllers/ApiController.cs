using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
    protected ActionResult Error(List<Error> errors)
    {
        if (errors.Count == 0) return Problem();

        return Error(errors[0]);
    }

    private ActionResult Error(Error error)
    {
        return Problem(statusCode: error.Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError
        }, title: error.Description);
    }
}