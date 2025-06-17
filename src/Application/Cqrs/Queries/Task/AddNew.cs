using Application.Repositories;
using ErrorOr;
using MediatR;
using Entity = Domain.Entities;

namespace Application.Cqrs.Queries.Task;

public record AddNewTaskCommand(Entity.Task Task, Guid UserId) : IRequest<ErrorOr<Success>>;

public class AddNewTaskCommandHandler(IUserRepository userRepository)
    : IRequestHandler<AddNewTaskCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(AddNewTaskCommand request,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId);

        if (user is null) return Error.NotFound($"User with ID `{request.UserId}` doesn't exist");

        user.AddTask(request.Task);
        await userRepository.UpdateAsync(user);

        return Result.Success;
    }
}