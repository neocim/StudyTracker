namespace Application.Dto.User;

public record UserResult(Guid Id)
{
    public static UserResult FromUser(Domain.Entities.User user)
    {
        return new UserResult(user.Id);
    }
}