using MediatR;
using DotnetExamSystem.Api.Models;

namespace DotnetExamSystem.Api.Application.Queries;

public class GetQuestionQuery : IRequest<Question>
{
    public string? Id { get; set; } 
}