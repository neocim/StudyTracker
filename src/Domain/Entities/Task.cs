namespace Domain.Entities;

public class Task : Entity
{
    public ICollection<Task> SubTasks { get; } = [];

    public Task? Parent { get; init; }
    public Guid? ParentId { get; init; }

    public Guid OwnerId { get; init; }

    public string Name { get; set; }
    public string? Description { get; set; }
    public bool? Success { get; set; }

    public DateOnly BeginDate { get; set; }
    public DateOnly EndDate { get; set; }

    private Task(Guid id) : base(id)
    {
    }

    public Task(Guid id, Guid ownerId, DateOnly beginDate,
        DateOnly endDate, string name, string? description = null,
        bool? success = null, Task? parent = null) : base(id)
    {
        Parent = parent;
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
}