using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Api.Dto.Requests.Task;

public record CreateTaskRequest
{
    [Required(ErrorMessage = "The name of the new task is required")]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }
    public bool? Success { get; set; }

    [Required(ErrorMessage = "The task begin date is required")]
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly BeginDate { get; set; }

    [Required(ErrorMessage = "The task end date is required")]
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly EndDate { get; set; }
}