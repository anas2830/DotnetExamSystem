namespace DotnetExamSystem.Api.DTO;

public class LoginResponse
{
    public string Token { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string UserRole { get; set; } = null!;
}