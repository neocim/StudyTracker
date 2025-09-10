using Application.Dto.Task.ReadModels;
using AutoMapper;
using Entity = Domain.Entities;
using Domain.Readers;
using MediatR;
using ErrorOr;

namespace Application.Cqrs.Queries.Task;

public record GetTaskByIdQuery(Guid TaskId) : IRequest<ErrorOr<TaskReadModel>>;

public class GetTaskByIdQueryHandler(ITaskReader taskReader, IMapper mapper)
    : IRequestHandler<GetTaskByIdQuery, ErrorOr<TaskReadModel>>
{
    public async Task<ErrorOr<TaskReadModel>> Handle(GetTaskByIdQuery request,
        CancellationToken cancellationToken)
    {
        var task = await taskReader.GetByIdAsync(request.TaskId);

        if (task is null)
            return Error.NotFound(
                description: $"Task with ID `{request.TaskId}` doesn't exist");

        return mapper.Map<Entity.Task, TaskReadModel>(task);
    }
}