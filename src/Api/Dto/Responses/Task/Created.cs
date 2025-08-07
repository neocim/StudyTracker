namespace Api.Dto.Responses.Task;

public record TaskCreatedResponse(Guid TaskId)
{
    public static TaskCreatedResponse FromTask(Domain.Entities.Task task)
    {
        return new TaskCreatedResponse(task.Id);
    }
}