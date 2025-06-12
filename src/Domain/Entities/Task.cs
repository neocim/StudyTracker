namespace Domain.Entities;

public class Task : Entity
{
    private readonly List<Guid> _subTaskIds = [];

    public DateOnly FromDate { get; private set; }
    public DateOnly ToDate { get; private set; }
    public bool Success;
    public string Name;

    public Task(Guid id, DateOnly fromDate,
        DateOnly toDate, string? name = null, bool? success = null) : base(id)
    {
        Name = name ?? RandomName(7);
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