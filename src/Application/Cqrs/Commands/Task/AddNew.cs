using Application.Repositories;
using ErrorOr;
using MediatR;
using Entity = Domain.Entities;

namespace Application.Cqrs.Commands.Task;

public record AddNewTaskCommand(Guid UserId, Entity.Task Task)
    : IRequest<ErrorOr<Created>>;

public class AddNewTaskCommandHandler(IUserRepository userRepository)
    : IRequestHandler<AddNewTaskCommand, ErrorOr<Created>>
{
    public async Task<ErrorOr<Created>> Handle(AddNewTaskCommand request,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId);

        if (user is null)
            return Error.NotFound($"User with ID `{request.UserId}` doesn't exist");

        user.AddTask(request.Task);
        await userRepository.UpdateAsync(user);

        return Result.Created;
    }
}