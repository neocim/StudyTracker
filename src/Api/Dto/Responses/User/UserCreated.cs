namespace Api.Dto.Responses.User;

public record UserCreatedResponse(Guid UserId)
{
    public static UserCreatedResponse FromUser(Domain.Entities.User user)
    {
        return new UserCreatedResponse(user.Id);
    }
}