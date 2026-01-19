using MediatR;

namespace DotnetExamSystem.Api.Application.Commands;

public class UpdateQuestionCommand : IRequest<bool>
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public Dictionary<string, string>? Options { get; set; }
    public string? CorrectAnswer { get; set; }
}