using System.ComponentModel.DataAnnotations;

namespace Api.Dto.Requests.Task;

public record UpdateTaskRequest
{
    [Required(ErrorMessage = "Task ID is required")]
    public Guid TaskId { get; set; }

    public bool? Success { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}