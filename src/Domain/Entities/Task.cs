namespace Domain.Entities;

public class Task : Entity
{
    public ICollection<Task> SubTasks { get; set; } = [];

    public readonly User Owner;
    public string Name { get; set; }
    public bool Success { get; set; }

    public DateOnly FromDate { get; set; }
    public DateOnly ToDate { get; set; }

    public Task(Guid id, User owner, DateOnly fromDate,
        DateOnly toDate, string? name = null, bool? success = null) : base(id)
    {
        Name = name ?? RandomName(7);
        Owner = owner;
        FromDate = fromDate;
        ToDate = toDate;
        Success = success ?? false;
    }

    private static string RandomName(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz1234567890";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}