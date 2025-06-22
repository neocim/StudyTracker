using System.Text.Json.Serialization;

namespace Api.Dto.Requests.Task;

public record GetTaskRequest
{
    [JsonRequired] public string TaskId { get; set; }
}