using DotnetExamSystem.Api.Models;
using MediatR;

public class BuyExamCommand : IRequest<UserExam>
{
    public string UserId { get; set; } = null!;
    public string ExamId { get; set; } = null!;
}

public class StartExamCommand : IRequest<UserExam>
{
    public string UserId { get; set; } = null!;
    public string ExamId { get; set; } = null!;
}

public class SubmitExamCommand : IRequest<UserExam>
{
    public string UserId { get; set; } = null!;
    public string ExamId { get; set; } = null!;
    public List<UserExamAnswer> Answers { get; set; } = new();
    public List<Question> Questions { get; set; } = new();
}
