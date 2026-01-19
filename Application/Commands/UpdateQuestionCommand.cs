using MediatR;

namespace DotnetExamSystem.Api.Application.Commands;

public class UpdateQuestionCommand : IRequest<bool>
{
    public string? Id { get; set; }
    public string Title { get; set; } = null!;
    public string Option1 { get; set; } = null!;
    public string Option2 { get; set; } = null!;
    public string Option3 { get; set; } = null!;
    public string Option4 { get; set; } = null!;
    public string CorrectAnswer { get; set; } = null!;
}