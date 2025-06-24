using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Api.Dto.Requests.Task;

public record AddSubTaskRequest
{
    [Required(ErrorMessage = "The ID of the main task is required")]
    public Guid MainTaskId { get; set; }

    [DefaultValue(null)] public string? Name { get; set; }
    [DefaultValue(null)] public string? Description { get; set; }
    [DefaultValue(null)] public bool? Success { get; set; }

    [Required(ErrorMessage = "The task begin date is required")]
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    [DateRange]
    public DateOnly BeginDate { get; set; }

    [Required(ErrorMessage = "The task end date is required")]
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    [DateRange]
    public DateOnly EndDate { get; set; }
}