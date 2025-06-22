using Application.Dto.User;
using Application.Repositories;
using MediatR;
using ErrorOr;

namespace Application.Cqrs.Queries.User;

public record GetUserByIdQuery(Guid UserId) : IRequest<ErrorOr<UserResult>>;

public class GetUserByIdQueryHandler(IUserRepository userRepository)
    : IRequestHandler<GetUserByIdQuery, ErrorOr<UserResult>>
{
    public async Task<ErrorOr<UserResult>> Handle(GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId);

        if (user is null)
            return Error.NotFound($"User with ID `{request.UserId}` doesn't exist");

        return UserResult.FromUser(user);
    }
}