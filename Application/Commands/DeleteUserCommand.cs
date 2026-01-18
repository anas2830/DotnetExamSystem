using MediatR;

namespace DotnetExamSystem.Api.Application.Commands;

public class DeleteUserCommand : IRequest<bool>
{
    public string? Id { get; set; }
}