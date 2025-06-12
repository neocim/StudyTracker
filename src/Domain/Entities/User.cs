namespace Domain.Entities;

public class User : Entity
{
    public ICollection<Task> Tasks { get; private set; } = [];

    public User(Guid id) : base(id)
    {
    }
}