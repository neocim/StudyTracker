namespace Domain.Entities;

public class User : Entity
{
    public readonly ICollection<Task> Tasks = [];

    public User(Guid id) : base(id)
    {
    }
}