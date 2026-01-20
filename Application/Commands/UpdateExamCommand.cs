using MediatR;
using DotnetExamSystem.Api.Models;

namespace DotnetExamSystem.Api.Application.Commands;

public class UpdateExamCommand : IRequest<bool>
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public DateTime? Date { get; set; }
    public decimal? Price { get; set; }
    public int? TimeInMinutes { get; set; }
    public int? TotalQuestions { get; set; }
}
