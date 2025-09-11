namespace Application.Authorization.Permissions;

public partial class Permission
{
    public static class Task
    {
        public const string CreateTask = "create:task";
        public const string CreateSubTask = "create:subtask";
        public const string Read = "read:task";
        public const string Update = "update:task";
        public const string Delete = "delete:task";
    }
}