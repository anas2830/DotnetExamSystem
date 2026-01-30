using MediatR;
using DotnetExamSystem.Api.Models;

namespace DotnetExamSystem.Api.Application.Queries;

public class GetAllExamsQuery() : IRequest<List<Exam>>
{
    public string? UserId { get; set; }
    public string? Role { get; set; }
}