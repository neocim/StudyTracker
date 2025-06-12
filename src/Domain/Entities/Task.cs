namespace Domain.Entities;

public abstract class Task : Entity
{
    private protected DateOnly FromDate;
    private protected DateOnly ToDate;
    public bool Success;

    protected Task(Guid id, DateOnly fromDate, DateOnly toDate, bool? success = null) :
        base(id)
    {
        FromDate = fromDate;
        ToDate = toDate;
        Success = success ?? false;
    }
}

public class MainTask : Task
{
    private readonly List<Guid> _subTaskIds = [];
    public string Name;

    public MainTask(Guid id, DateOnly fromDate,
        DateOnly toDate, string? name = null) : base(id, fromDate, toDate)
    {
        Name = name ?? RandomName(7);
    }

    private static string RandomName(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz1234567890";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}