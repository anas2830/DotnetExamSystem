using DotnetExamSystem.Api.Models;
using MediatR;

namespace DotnetExamSystem.Api.Application.Commands;

public class CreateUserCommand : IRequest<User>
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
} 