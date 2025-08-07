using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Api.Dto.Requests.Task;

public record NewTaskRequest
{
    [Required(ErrorMessage = "The ID of the user who will own the new task is required")]
    public Guid OwnerId { get; set; }

    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool? Success { get; set; }

    [Required(ErrorMessage = "The task begin date is required")]
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly BeginDate { get; set; }

    [Required(ErrorMessage = "The task end date is required")]
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly EndDate { get; set; }
}