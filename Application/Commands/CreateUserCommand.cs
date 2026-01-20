using DotnetExamSystem.Api.Models;
using MediatR;

namespace DotnetExamSystem.Api.Application.Commands;

public class CreateUserCommand : IRequest<User>
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Mobile { get; set; }
    public string? Address { get; set; }
    public IFormFile? ProfileImage { get; set; }
    public decimal Balance { get; set; }
} 