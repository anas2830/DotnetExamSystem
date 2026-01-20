using MediatR;

namespace DotnetExamSystem.Api.Application.Commands;

public class DeleteExamCommand : IRequest<bool>
{
    public string Id { get; set; } = null!;
}
