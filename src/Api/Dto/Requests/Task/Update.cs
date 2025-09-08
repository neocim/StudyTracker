namespace Api.Dto.Requests.Task;

public record UpdateTaskRequest
{
    public bool? Success { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}