namespace Application.Security.Permissions;

public static partial class Permission
{
    public static class Task
    {
        public const string Create = "create:task";
        public const string Read = "read:task";
        public const string Update = "update:task";
        public const string Delete = "delete:task";
    }
}