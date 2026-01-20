using MediatR;
using DotnetExamSystem.Api.Models;

namespace DotnetExamSystem.Api.Application.Commands;

public class CreateExamCommand : IRequest<Exam>
{
    public string Title { get; set; } = null!;
    public DateTime Date { get; set; }
    public decimal Price { get; set; }
    public int TimeInMinutes { get; set; }
    public int TotalQuestions { get; set; }
}
