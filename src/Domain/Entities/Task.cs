namespace Domain.Entities;

public class Task : Entity
{
    public ICollection<Task> SubTasks { get; } = [];

    public User Owner { get; init; }
    public Guid OwnerId { get; init; }

    public string Name { get; set; }
    public string? Description { get; set; }
    public bool? Success { get; set; }

    public DateOnly BeginDate { get; set; }
    public DateOnly EndDate { get; set; }

    public Task(Guid id, DateOnly beginDate,
        DateOnly endDate, string? description = null, string? name = null,
        bool? success = null) : base(id)
    {
        Name = name ?? RandomName(7);
        Description = description;
        Success = success;
        BeginDate = BeginDate;
        EndDate = EndDate;
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