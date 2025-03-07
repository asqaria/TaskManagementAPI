namespace TaskManagementAPI.Services.Interface
{
    public interface IJwtService{
        string GenerateSecurityToken(string username, string role);
    }
}