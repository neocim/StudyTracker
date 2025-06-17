namespace Domain.Entities;

public class User : Entity
{
    public ICollection<Task> Tasks { get; } = [];

    public User(Guid id) : base(id)
    {
    }

    public void AddTask(Task task)
    {
        Tasks.Add(task);
    }
}