namespace Application.Authorization.Permissions;

public static class TaskPermission
{
    public const string CreateTask = "create:task";
    public const string CreateSubTask = "create:subtask";
    public const string Read = "read:task";
    public const string Update = "update:task";
    public const string Delete = "delete:task";
}