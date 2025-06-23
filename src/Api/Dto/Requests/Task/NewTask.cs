using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Api.Dto.Requests.Task;

public record NewTaskRequest
{
    [Required(ErrorMessage = "The ID of the user who will own the new task is required")]
    public Guid OwnerId { get; set; }

    [DefaultValue(null)] public string? Name { get; set; }
    [DefaultValue(null)] public string? Description { get; set; }

    [Required(ErrorMessage = "The task begin date is required")]
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    [DateRange]
    public DateOnly FromDate { get; set; }

    [Required(ErrorMessage = "The task end date is required")]
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    [DateRange]
    public DateOnly ToDate { get; set; }
}