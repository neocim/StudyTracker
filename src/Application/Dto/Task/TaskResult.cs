namespace Application.Dto.Task;

public record TaskResult(
    Guid OwnerId,
    string Name,
    string? Description,
    bool? Success,
    DateOnly FromDate,
    DateOnly ToDate)
{
    public static TaskResult FromTask(Domain.Entities.Task task)
    {
        return new TaskResult(task.OwnerId, task.Name,
            task.Description, task.Success, task.FromDate, task.ToDate);
    }
}