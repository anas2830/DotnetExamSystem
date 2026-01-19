using MediatR;
using DotnetExamSystem.Api.Models;

namespace DotnetExamSystem.Api.Application.Commands;

public class CreateQuestionCommand : IRequest<Question>
{
    public string Title { get; set; } = null!;
    public string Option1 { get; set; } = null!;
    public string Option2 { get; set; } = null!;
    public string Option3 { get; set; } = null!;
    public string Option4 { get; set; } = null!;
    public string CorrectAnswer { get; set; } = null!;
}
