using Application.Dto.Task.ReadModels;
using Application.Security;
using Application.Security.Permissions;
using AutoMapper;
using Entity = Domain.Entities;
using Domain.Readers;
using MediatR;
using ErrorOr;
using Microsoft.Extensions.Logging;

namespace Application.Cqrs.Queries.Task;

public record GetTaskByIdQuery(Guid TaskId) : IRequest<ErrorOr<TaskReadModel>>;

public class GetTaskByIdQueryHandler(
    ITaskReader taskReader,
    IMapper mapper,
    ISecurityContext securityContext,
    ILogger<GetTaskByIdQueryHandler> logger)
    : IRequestHandler<GetTaskByIdQuery, ErrorOr<TaskReadModel>>
{
    public async Task<ErrorOr<TaskReadModel>> Handle(GetTaskByIdQuery request,
        CancellationToken cancellationToken)
    {
        if (!securityContext.HasPermission(Permission.Task.Read))
            return Error.Forbidden(description: "Access denied");

        var task = await taskReader.GetByIdAsync(request.TaskId);

        if (task is null)
            return Error.NotFound(
                description: $"Task with ID `{request.TaskId}` doesn't exist");

        logger.LogInformation($"Get the task `{request.TaskId}`");

        return mapper.Map<Entity.Task, TaskReadModel>(task);
    }
}