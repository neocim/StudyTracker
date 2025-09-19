using Application.Dto.Task.ReadModels;
using Application.Security;
using Application.Security.Permissions;
using AutoMapper;
using Domain.Readers;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using Entity = Domain.Entities;

namespace Application.Cqrs.Queries.Task;

public record GetByUserIdQuery(Guid UserId)
    : IRequest<ErrorOr<IEnumerable<TaskNodeReadModel>?>>;

public class GetByUserIdQueryHandler(
    ITaskReader taskReader,
    IMapper mapper,
    ISecurityContext securityContext,
    ILogger<GetByUserIdQueryHandler> logger)
{
    public ErrorOr<IEnumerable<TaskNodeReadModel>?> Handle(
        GetByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        if (!securityContext.HasPermission(Permission.Task.Read))
            return Error.Forbidden(description: "Access denied");

        var tasks = taskReader.GetByUserId(request.UserId);

        if (tasks is null)
            return Error.NotFound(
                description: $"User with ID `{request.UserId}` has no tasks");

        logger.LogInformation($"User `{request.UserId}` gets the tasks");

        return mapper
            .Map<IEnumerable<Entity.Task>, IEnumerable<TaskNodeReadModel>>(tasks)
            .ToErrorOr()!;
    }
}