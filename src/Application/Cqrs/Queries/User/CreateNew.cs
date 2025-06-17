using Application.Repositories;
using Entity = Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.Cqrs.Queries.User;

public record CreateNewUserCommand(Entity.User User) : IRequest<ErrorOr<Success>>;

public class CreateNewUserCommandHandler(IUserRepository userRepository)
    : IRequestHandler<CreateNewUserCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(CreateNewUserCommand request,
        CancellationToken cancellationToken)
    {
        if (await userRepository.GetByIdAsync(request.User.Id) is not null)
            return Error.Conflict($"User with ID `{request.User.Id}` already exists");

        await userRepository.AddAsync(request.User);

        return Result.Success;
    }
}