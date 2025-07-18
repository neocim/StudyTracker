using System.ComponentModel.DataAnnotations;

namespace Api.Dto.Requests.Task;

public record GetTaskRequest
{
    [Required(ErrorMessage = "Task ID is required to get the task")]
    public Guid TaskId { get; set; }
}