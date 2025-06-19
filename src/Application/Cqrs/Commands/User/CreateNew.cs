using Application.Repositories;
using Entity = Domain.Entities;
using ErrorOr;
using MediatR;

namespace Application.Cqrs.Commands.User;

public record CreateNewUserCommand(Entity.User User) : IRequest<ErrorOr<Created>>;

public class CreateNewUserCommandHandler(IUserRepository userRepository)
    : IRequestHandler<CreateNewUserCommand, ErrorOr<Created>>
{
    public async Task<ErrorOr<Created>> Handle(CreateNewUserCommand request,
        CancellationToken cancellationToken)
    {
        if (await userRepository.GetByIdAsync(request.User.Id) is not null)
            return Error.Conflict($"User with ID `{request.User.Id}` already exists");

        await userRepository.AddAsync(request.User);

        return Result.Created;
    }
}