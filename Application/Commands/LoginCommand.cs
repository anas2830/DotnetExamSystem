using MediatR;
using DotnetExamSystem.Api.DTO;

namespace DotnetExamSystem.Api.Application.Commands;

public class LoginCommand : IRequest<LoginResponse>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
