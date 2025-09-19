namespace Api.Dto.Responses.Task;

public record TaskResponse(
    Guid Id,
    Guid OwnerId,
    Guid? ParentId,
    DateOnly BeginDate,
    DateOnly EndDate,
    string Name,
    string? Description,
    bool? Success);