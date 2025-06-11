namespace Domain.Entities;

public class Task : Entity
{
    private readonly List<Guid> _subTaskIds = [];
    private DateOnly TaskBeginTime;
    private DateOnly TaskEndTime;

    public string TaskName = null!;
    public bool Success = false;

    public Task(Guid id, DateOnly taskBeginTime,
        DateOnly taskEndTime, string? taskName = null) : base(id)
    {
        TaskBeginTime = taskBeginTime;
        TaskEndTime = taskEndTime;
        TaskName = taskName ?? RandomName(7);
    }

    private static string RandomName(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz1234567890";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}

public class SubTask : Entity
{
    private DateOnly TaskBeginTime;
    private DateOnly TaskEndTime;

    public SubTask(Guid id) : base(id)
    {
    }
}