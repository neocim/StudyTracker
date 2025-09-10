namespace Application.Dto.Task.ReadModels;

public record TaskReadModel(
    Guid Id,
    Guid OwnerId,
    DateOnly BeginDate,
    DateOnly EndDate,
    string Name,
    string? Description,
    bool? Success);