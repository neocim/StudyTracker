namespace Domain.Entities;

public class Task : Entity
{
    public ICollection<Task> SubTasks { get; } = [];

    public Guid OwnerId { get; init; }

    public string Name { get; set; }
    public string? Description { get; set; }
    public bool? Success { get; set; }

    public DateOnly BeginDate { get; set; }
    public DateOnly EndDate { get; set; }

    public Task(Guid id, Guid ownerId, DateOnly beginDate,
        DateOnly endDate, string name, string? description = null,
        bool? success = null) : base(id)
    {
        OwnerId = ownerId;
        Name = name;
        Description = description;
        Success = success;
        BeginDate = beginDate;
        EndDate = endDate;
    }

    public void AddSubTask(Task subTask)
    {
        SubTasks.Add(subTask);
    }

    private static string RandomName(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz1234567890";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}