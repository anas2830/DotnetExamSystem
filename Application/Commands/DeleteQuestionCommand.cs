using MediatR;

namespace DotnetExamSystem.Api.Application.Commands;

public class DeleteQuestionCommand : IRequest<bool>
{
    public string Id { get; set; } = null!;
}
