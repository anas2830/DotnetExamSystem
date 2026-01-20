using MediatR;
using DotnetExamSystem.Api.Models;

namespace DotnetExamSystem.Api.Application.Queries;

public class GetExamQuery : IRequest<Exam>
{
    public string? Id { get; set; } 
}