namespace Api.Dto.Responses.Task;

public record TaskCreatedResult(Guid TaskId)
{
    public static TaskCreatedResult FromTask(Domain.Entities.Task task)
    {
        return new TaskCreatedResult(task.Id);
    }
}