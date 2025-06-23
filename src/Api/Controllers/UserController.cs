using Api.Controllers;
using Api.Dto.Requests.User;
using Api.Dto.Responses.User;
using Application.Cqrs.Commands.User;
using Entity = Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using MediatR;

[Route("user")]
public class UserController(IMediator mediator) : ApiController
{
    [HttpPost]
    public async Task<ActionResult<UserCreatedResponse>> NewUser(NewUserRequest _)
    {
        var user = new Entity.User(Guid.NewGuid());

        var command = new CreateNewUserCommand(user);
        var result = await mediator.Send(command);

        return result.Match(_ => Ok(UserCreatedResponse.FromUser(user)), Error);
    }
}