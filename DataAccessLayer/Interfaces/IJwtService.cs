namespace DotnetExamSystem.Api.DataAccessLayer.Interfaces;

public interface IJwtService
{
    string GenerateToken(string userId, string userName, string userRole);
}