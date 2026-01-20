using MediatR;

namespace DotnetExamSystem.Api.Application.Commands;

public class UpdateUserCommand : IRequest<bool>
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Password { get; set; }
    public string? Mobile { get; set; }
    public string? Address { get; set; }
    public IFormFile? ProfileImage { get; set; }
    public decimal Balance { get; set; }
}