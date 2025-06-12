namespace Domain.Entities;

public class User : Entity
{
    public readonly ICollection<Task> UserTasks = [];

    public User(Guid id) : base(id)
    {
    }
}