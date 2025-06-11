namespace Domain.Entities;

public class User : Entity
{
    private List<Guid> _userTaskIds = [];

    public User(Guid id) : base(id)
    {
    }
}