using MediatR;
using DotnetExamSystem.Api.Models;

namespace DotnetExamSystem.Api.Application.Queries;

public class GetAllExamsQuery() : IRequest<List<Exam>>
{
    public string? UserId { get; set; }
    public string? Role { get; set; }
    public string? Search { get; set; } = null;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}