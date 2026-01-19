using MediatR;

namespace DotnetExamSystem.Api.Application.Commands;

public class UpdateUserCommand : IRequest<bool>
{
    public string? Id { get; set; }
    public string? Name { get; set; } = null!;
    public string? Password { get; set; } = null!;
    public string? Mobile { get; set; }
    public string? Address { get; set; }
    public IFormFile? ProfileImage { get; set; }
}