namespace Api.Dto.Responses.Task;

public record TaskCreatedResponse(Guid TaskId)
{
    public static TaskCreatedResponse FromTaskEntity(Domain.Entities.Task task)
    {
        return new TaskCreatedResponse(task.Id);
    }
}