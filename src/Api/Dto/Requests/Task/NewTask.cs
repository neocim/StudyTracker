using System.Text.Json.Serialization;

namespace Api.Dto.Requests.Task;

public record NewTaskRequest
{
    [JsonRequired] public Guid OwnerId { get; set; }

    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool? Success { get; set; }

    [JsonRequired]
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly FromDate { get; set; }

    [JsonRequired]
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly ToDate { get; set; }
}