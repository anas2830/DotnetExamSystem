using MediatR;
using DotnetExamSystem.Api.Models;

namespace DotnetExamSystem.Api.Application.Commands;

public class CreateQuestionCommand : IRequest<Question>
{
    public string Title { get; set; } = null!;
    public Dictionary<string, string> Options { get; set; } = new();
    public string CorrectAnswer { get; set; } = null!;
}
