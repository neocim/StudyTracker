using System.ComponentModel.DataAnnotations;

namespace Api.Dto.Requests.Task;

public record UpdateTaskStatusRequest
{
    [Required(ErrorMessage = "Task ID is required")]
    public Guid TaskId { get; set; }

    [Required(ErrorMessage = "You must specify whether the task is successful or not")]
    public bool Success { get; set; }
}