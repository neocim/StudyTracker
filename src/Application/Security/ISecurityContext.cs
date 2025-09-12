namespace Application.Security;

public interface ISecurityContext
{
    public bool HasPermission(string permission);
}