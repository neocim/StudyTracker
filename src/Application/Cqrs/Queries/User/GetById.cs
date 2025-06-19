using Application.Repositories;
using MediatR;
using Entity = Domain.Entities;
using ErrorOr;

namespace Application.Cqrs.Queries.User;

public record GetUserByIdQuery(Guid UserId) : IRequest<ErrorOr<Entity.User>>;

public class GetUserByIdQueryHandler(IUserRepository userRepository)
    : IRequestHandler<GetUserByIdQuery, ErrorOr<Entity.User>>
{
    public async Task<ErrorOr<Entity.User>> Handle(GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId);

        if (user is null)
            return Error.NotFound($"User with ID `{request.UserId}` doesn't exist");

        return user;
    }
}