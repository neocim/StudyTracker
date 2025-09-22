using System.Text.Json.Serialization;

namespace Api.Dto.Requests.Task;

public record UpdateTaskRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool? Success { get; set; }

    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly? BeginDate { get; set; }

    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly? EndDate { get; set; }
}